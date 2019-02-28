using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Swagify.APIs
{
    class HttpSignupRequest
    {
        public HttpClient client;

        public HttpSignupRequest(HttpClient httpClient)
        {
            client = httpClient;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> PostAccountCreation(String username, String password)
        {
            String uri = "signup";
            JObject credentials = new JObject(
                new JProperty("username", username),
                new JProperty("password", password)
            );

            var json = JsonConvert.SerializeObject(credentials);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);

            return response.IsSuccessStatusCode;
        }
    }
}
