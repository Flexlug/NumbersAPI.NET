// MIT License

// Copyright (c) 2021 Flexlug

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using NumbersAPI.NET.Exceptions;
using NumbersAPI.NET.Models;

namespace NumbersAPI.NET.Net
{
    internal class HttpWorker
    {
        private const string BASE_URL = "http://numbersapi.com";

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
