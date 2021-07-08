using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using NumbersAPI.NET.Exceptions;
using NumbersAPI.NET.Models;

namespace NumbersAPI.NET.Net
{
    internal class HttpWorker
    {
        private const string BASE_URL = "https://numbersapi.com";

        private readonly HttpClient client;

        internal HttpWorker()
        {
            client = new HttpClient();
        }

        internal HttpWorker(HttpClient client)
        {
            this.client = client;
        }

        internal async Task<string> GetResponse(string par, string endpoint)
        {
            var response = await client.GetAsync($"{BASE_URL}/{par}/{endpoint}?json");

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new HttpRequestException($"Can not recieve response. Code: {response.StatusCode}");

            var str_response = await response.Content.ReadAsStringAsync();
            var obj_response = JsonSerializer.Deserialize<NumbersResponse>(str_response);

            if (!obj_response.Found)
                throw new NumbersNotFoundException();

            return obj_response.Text;
        }
    }
}
