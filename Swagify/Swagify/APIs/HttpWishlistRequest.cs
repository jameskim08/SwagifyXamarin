using Newtonsoft.Json.Linq;
using Swagify;
using Swagify.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Swagify
{
    class HttpWishlistRequest : BaseHttpRequest
    {
        public HttpWishlistRequest(HttpClient httpClient) : base(httpClient) { }

        public async Task<List<Product>> GetWishlist()
        {
            string uri = "wishlist";
            List<Product> wishlist = new List<Product>();

            SetBaseHeaders(client);
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                JObject jsonObject = JObject.Parse(content);
                IList<JToken> results = jsonObject["data"].Children().ToList();

                foreach (JToken result in results)
                {
                    Product product = result.ToObject<Product>();
                    wishlist.Add(product);
                }
            }
            return wishlist;

        }

        public async Task<HttpStatusCode> PostProductToWishlist(string id)
        {
            String uri = String.Format("wishlist/{0}", id);
            SetBaseHeaders(client);
            JObject credentials = new JObject(
               new JProperty("id", id)
            );
            HttpResponseMessage response = await PostRequest(uri, credentials);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteProductFromWishlist(string id)
        {
            String uri = String.Format("wishlist/{0}", id);
            SetBaseHeaders(client);
            HttpResponseMessage response = await client.DeleteAsync(uri);
            return response.StatusCode;
        }
    }
}
