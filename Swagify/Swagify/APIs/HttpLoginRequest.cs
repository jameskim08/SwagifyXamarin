using Newtonsoft.Json.Linq;
using Swagify;
using Swagify.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Swagify
{
    class HttpLoginRequest : BaseHttpRequest
    {
        public HttpLoginRequest(HttpClient httpClient) : base(httpClient) { }

        public async Task<bool> GetLoginUser(String email, String password)
        {
            String uri = "token";

            var authData = string.Format("{0}:{1}", email, password);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                dynamic data = JObject.Parse(content);

                string token = data.token;
                string name = data.name;

                try
                {
                    await SecureStorage.SetAsync("oauth_token", token);
                }
                catch (Exception ex)
                {
                    Application.Current.Properties["token"] = token;
                    Console.Write(ex.Message);
                }

                User user = new User
                {
                    Name = name,
                    Email = email,
                };

                Application.Current.Properties["user"] = user;
            }

            return response.IsSuccessStatusCode;
        }
    }
}
