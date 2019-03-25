using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Swagify.APIs
{
    class HttpSignupRequest : BaseHttpRequest
    {
        public HttpSignupRequest(HttpClient httpClient) : base(httpClient) { }

        public async Task<bool> PostAccountCreation(String name, String email, String password)
        {
            String uri = String.Format("users?email={0}&password={1}&name={2}", email, password, name);
            JObject credentials = new JObject(
                new JProperty("name", name),
                new JProperty("email", email),
                new JProperty("password", password)
            );
            HttpResponseMessage response = await PostRequest(uri, credentials);
            return response.IsSuccessStatusCode;
        }
    }
}
