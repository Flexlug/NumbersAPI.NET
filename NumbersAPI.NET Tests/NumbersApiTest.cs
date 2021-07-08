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

using NUnit.Framework;

using RichardSzalay.MockHttp;

using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using NumbersAPI.NET;
using NumbersAPI.NET.Exceptions;

namespace NumbersAPI.NET_Tests
{
    public class NumbersApiTest
    {
        private const string BASE_URL = "http://numbersapi.com";

        [SetUp]
        public void Setup() { }

        [Test]
        public async Task RandomTextTest()
        {
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When($"{BASE_URL}/random/trivia?json")
                    .Respond("application/json", "{ \"text\": \"sample trivia\", \"found\": true, \"type\": \"year\", \"date\": \"June 6\" }");

            mockHttp.When($"{BASE_URL}/random/year?json")
                    .Respond("application/json", "{ \"text\": \"sample year\", \"found\": true, \"type\": \"year\", \"date\": \"June 6\" }");

            mockHttp.When($"{BASE_URL}/random/date?json")
                    .Respond("application/json", "{ \"text\": \"sample date\", \"found\": true, \"type\": \"year\", \"date\": \"June 6\" }");

            mockHttp.When($"{BASE_URL}/random/math?json")
                    .Respond("application/json", "{ \"text\": \"sample math\", \"found\": true, \"type\": \"year\", \"date\": \"June 6\" }");

            var api = new NumbersApi(mockHttp.ToHttpClient());

            Assert.AreEqual(await api.RandomTriviaAsync(), "sample trivia");
            Assert.AreEqual(await api.RandomYearAsync(), "sample year");
            Assert.AreEqual(await api.RandomDateAsync(), "sample date");
            Assert.AreEqual(await api.RandomMathAsync(), "sample math");
        }

        [Test]
        public async Task RandomHttpExceptionHandleTest()
        {
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When($"{BASE_URL}/random/trivia?json")
                    .Respond(HttpStatusCode.BadRequest);

            mockHttp.When($"{BASE_URL}/random/year?json")
                    .Respond(HttpStatusCode.BadRequest);

            mockHttp.When($"{BASE_URL}/random/date?json")
                    .Respond(HttpStatusCode.BadRequest);

            mockHttp.When($"{BASE_URL}/random/math?json")
                    .Respond(HttpStatusCode.BadRequest);

            var api = new NumbersApi(mockHttp.ToHttpClient());

            Assert.ThrowsAsync<HttpRequestException>(async () => { string resp = await api.RandomTriviaAsync(); });
            Assert.ThrowsAsync<HttpRequestException>(async () => { string resp = await api.RandomYearAsync(); });
            Assert.ThrowsAsync<HttpRequestException>(async () => { string resp = await api.RandomDateAsync(); });
            Assert.ThrowsAsync<HttpRequestException>(async () => { string resp = await api.RandomMathAsync(); });
        }

        [Test]
        public async Task RandomNotFoundExceptionHandleTest()
        {
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When($"{BASE_URL}/random/trivia?json")
                    .Respond("application/json", "{ \"text\": \"sample text\", \"found\": false, \"type\": \"year\", \"date\": \"June 6\" }");

            mockHttp.When($"{BASE_URL}/random/year?json")
                    .Respond("application/json", "{ \"text\": \"sample year\", \"found\": false, \"type\": \"year\", \"date\": \"June 6\" }");

            mockHttp.When($"{BASE_URL}/random/date?json")
                    .Respond("application/json", "{ \"text\": \"sample date\", \"found\": false, \"type\": \"year\", \"date\": \"June 6\" }");

            mockHttp.When($"{BASE_URL}/random/math?json")
                    .Respond("application/json", "{ \"text\": \"sample math\", \"found\": false, \"type\": \"year\", \"date\": \"June 6\" }");

            var api = new NumbersApi(mockHttp.ToHttpClient());

            Assert.ThrowsAsync<NumbersNotFoundException>(async () => { string resp = await api.RandomTriviaAsync(); });
            Assert.ThrowsAsync<NumbersNotFoundException>(async () => { string resp = await api.RandomYearAsync(); });
            Assert.ThrowsAsync<NumbersNotFoundException>(async () => { string resp = await api.RandomDateAsync(); });
            Assert.ThrowsAsync<NumbersNotFoundException>(async () => { string resp = await api.RandomMathAsync(); });
        }

        [Test]
        public async Task TextTest()
        {
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When($"{BASE_URL}/11/trivia?json")
                    .Respond("application/json", "{ \"text\": \"sample trivia\", \"found\": true, \"type\": \"year\", \"date\": \"June 6\" }");

            mockHttp.When($"{BASE_URL}/22/year?json")
                    .Respond("application/json", "{ \"text\": \"sample year\", \"found\": true, \"type\": \"year\", \"date\": \"June 6\" }");

            mockHttp.When($"{BASE_URL}/3/3/date?json")
                    .Respond("application/json", "{ \"text\": \"sample date\", \"found\": true, \"type\": \"year\", \"date\": \"June 6\" }");

            mockHttp.When($"{BASE_URL}/44/math?json")
                    .Respond("application/json", "{ \"text\": \"sample math\", \"found\": true, \"type\": \"year\", \"date\": \"June 6\" }");

            var api = new NumbersApi(mockHttp.ToHttpClient());

            Assert.AreEqual(await api.TriviaAsync(11), "sample trivia");
            Assert.AreEqual(await api.YearAsync(22), "sample year");
            Assert.AreEqual(await api.DateAsync(new System.DateTime(2000, 3, 3)), "sample date");
            Assert.AreEqual(await api.MathAsync(44), "sample math");
        }

        [Test]
        public async Task HttpExceptionHandleTest()
        {
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When($"{BASE_URL}/1/trivia?json")
                    .Respond(HttpStatusCode.BadRequest);

            mockHttp.When($"{BASE_URL}/2/year?json")
                    .Respond(HttpStatusCode.BadRequest);

            mockHttp.When($"{BASE_URL}/3/3/date?json")
                    .Respond(HttpStatusCode.BadRequest);

            mockHttp.When($"{BASE_URL}/4/math?json")
                    .Respond(HttpStatusCode.BadRequest);

            var api = new NumbersApi(mockHttp.ToHttpClient());

            Assert.ThrowsAsync<HttpRequestException>(async () => { string resp = await api.TriviaAsync(1); });
            Assert.ThrowsAsync<HttpRequestException>(async () => { string resp = await api.YearAsync(2); });
            Assert.ThrowsAsync<HttpRequestException>(async () => { string resp = await api.DateAsync(new System.DateTime(2000, 3, 3)); });
            Assert.ThrowsAsync<HttpRequestException>(async () => { string resp = await api.MathAsync(4); });
        }

        [Test]
        public async Task NotFoundExceptionHandleTest()
        {
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When($"{BASE_URL}/1/trivia?json")
                    .Respond("application/json", "{ \"text\": \"sample text\", \"found\": false, \"type\": \"year\", \"date\": \"June 6\" }");

            mockHttp.When($"{BASE_URL}/2/year?json")
                    .Respond("application/json", "{ \"text\": \"sample year\", \"found\": false, \"type\": \"year\", \"date\": \"June 6\" }");

            mockHttp.When($"{BASE_URL}/3/3/date?json")
                    .Respond("application/json", "{ \"text\": \"sample date\", \"found\": false, \"type\": \"year\", \"date\": \"June 6\" }");

            mockHttp.When($"{BASE_URL}/4/math?json")
                    .Respond("application/json", "{ \"text\": \"sample math\", \"found\": false, \"type\": \"year\", \"date\": \"June 6\" }");

            var api = new NumbersApi(mockHttp.ToHttpClient());

            Assert.ThrowsAsync<NumbersNotFoundException>(async () => { string resp = await api.TriviaAsync(1); });
            Assert.ThrowsAsync<NumbersNotFoundException>(async () => { string resp = await api.YearAsync(2); });
            Assert.ThrowsAsync<NumbersNotFoundException>(async () => { string resp = await api.DateAsync(new System.DateTime(2000, 3, 3)); });
            Assert.ThrowsAsync<NumbersNotFoundException>(async () => { string resp = await api.MathAsync(4); });
        }
    }
}