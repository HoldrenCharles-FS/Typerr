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
        private static readonly string _keyKey = "X-RapidAPI-Key";
        private static readonly string _hostKey = "X-RapidAPI-Host";

        internal static async Task<TestModel> GetTestByUrl(string url)
        {
            string uri = _url + url + _parameters;
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri),
                Headers =
    {
        { _keyKey, "95bcb5e033mshd03d07cb0fe4f39p10b390jsna76a26d9cc29" },
        { _hostKey, "lexper.p.rapidapi.com" },
    },
            };
            TestModel testModel;
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                testModel =
                JsonConvert.DeserializeObject<TestModel>(json);

            }
            return testModel;
        }
    }
}
