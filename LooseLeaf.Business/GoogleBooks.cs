using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LooseLeaf.Business.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LooseLeaf.Business
{
    public class GoogleBooks
    {
        public async Task<IBook> GetBookFromIsbn(long isbn)
        {
            string baseAddress = "https://www.googleapis.com/books/v1/volumes";
            string urlParameters = $"?q=isbn:{isbn}";

            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                string jsonString = await response.Content.ReadAsStringAsync();
                JObject queryResults = JObject.Parse(jsonString);
                var volumeInfoJson = queryResults["items"].First()["volumeInfo"];

                VolumeInfo info = volumeInfoJson.ToObject<VolumeInfo>();

                return new Book(info.Title, info.Authors[0], isbn, 1);
            }
            else
                return null;
        }

        private class VolumeInfo
        {
            public string Title { get; set; }
            public string[] Authors { get; set; }

            public DateTime PublishedDate { get; set; }
        }
    }
}