using Newtonsoft.Json.Linq;
using Swagify;
using Swagify.Models;
using System;
using System.Collections.Generic;
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
    class HttpSearchRequest : BaseHttpRequest
    {
        public HttpSearchRequest(HttpClient httpClient) : base(httpClient) { }

        public async Task<List<Product>> GetSearchList(string searchText)
        {
            List<Product> productList = new List<Product>();
            String uri = String.Format("item/?name={0}", searchText);
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
                    productList.Add(product);
                }
            }
            return productList;
        }
    }
}
