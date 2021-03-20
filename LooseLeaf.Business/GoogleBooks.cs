using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LooseLeaf.Business.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LooseLeaf.Business
{
    public class GoogleBooks
    {
        private readonly HttpClient _client;

        private string baseUrlParameters = "?";

        public GoogleBooks(HttpClient client, IOptions<GoogleBooksOptions> options)
        {
            if (options is not null)
            {
                string apiKey = $"key={options.Value.ApiKey}&";
                baseUrlParameters += apiKey;
            }

            client.BaseAddress = new Uri("https://www.googleapis.com/books/v1/volumes");
            _client = client;
        }

        public async Task<IBook> GetBookFromIsbn(long isbn)
        {
            string urlParameters = baseUrlParameters + $"q=isbn:{isbn}";

            _client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await _client.GetAsync(urlParameters);
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                string jsonString = await response.Content.ReadAsStringAsync();
                JObject queryResults = JObject.Parse(jsonString);

                if (queryResults["totalItems"].ToObject<int>() == 0)
                    throw new ArgumentException("ISBN not found in Google Books");

                var volumeInfoJson = queryResults["items"].First()["volumeInfo"];

                VolumeInfo info = volumeInfoJson.ToObject<VolumeInfo>();

                return new Book(info.Title, info.Authors[0], isbn, info.Categories);
            }
            else
                throw new HttpRequestException("Cannot connect to Google Books");
        }

        private class VolumeInfo
        {
            public string Title { get; set; }
            public string[] Authors { get; set; }

            public string[] Categories { get; set; }
        }
    }

    public class GoogleBooksOptions
    {
        public static readonly string ApiKeyConfiguration = "GoogleBooksApiKey";
        public string ApiKey { get; set; }
    }
}