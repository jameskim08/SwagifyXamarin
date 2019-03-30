using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    class BaseHttpRequest
    {
        public HttpClient client;

        public BaseHttpRequest (HttpClient httpClient)
        {
            client = httpClient;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HttpResponseMessage> PostRequest(string uri, JObject data)
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await client.PostAsync(uri, content);
        }

        public static async Task<string> GetToken()
        {
            try
            {
                return await SecureStorage.GetAsync("oauth_token");
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                if (Application.Current.Properties.ContainsKey("token"))
                {
                    return (string)Application.Current.Properties["token"];
                }
                return "";
            }
        }

        public static async void SetBaseHeaders(HttpClient client)
        {
            string token = await GetToken();
            var authData = string.Format("{0}:", token);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        } 
    }
}
