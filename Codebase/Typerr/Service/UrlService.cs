using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Typerr.Model;

namespace TyperrDemo.Services
{
    public class UrlService
    {
        static HttpClient _client = new HttpClient();
        private static readonly string _url = "https://lexper.p.rapidapi.com/v1.1/extract?url=";
        private static readonly string _parameters = "&js_timeout=30&media=true";

        // Get request test data from the Article Data Extraction and Text Mining API
        internal static async Task<TestModel> GetTestByUrl(string url)
        {
            string uri = _url + url + _parameters;

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri),
                Headers =
    {
        { "X-RapidAPI-Key", "95bcb5e033mshd03d07cb0fe4f39p10b390jsna76a26d9cc29" },
        { "X-RapidAPI-Host", "lexper.p.rapidapi.com" },
    },
            };
            TestModel testModel;
            using (var response = await _client.SendAsync(request))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    testModel = new TestModel();
                    testModel.Base64Image = nameof(System.Net.HttpStatusCode.TooManyRequests);
                    return testModel;
                }
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                testModel =
                JsonConvert.DeserializeObject<TestModel>(json);

            }
            return testModel;
        }
    }
}
