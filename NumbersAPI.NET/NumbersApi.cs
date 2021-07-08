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

using System;
using System.Net.Http;
using System.Threading.Tasks;

using NumbersAPI.NET.Net;
using NumbersAPI.NET.Exceptions;

namespace NumbersAPI.NET
{
    /// <summary>
    /// NumbersAPI implementation
    /// </summary>
    public class NumbersApi
    {
        private readonly HttpWorker Client;

        public NumbersApi()
        {
            Client = new HttpWorker();
        }

        public NumbersApi(HttpClient client)
        {
            Client = new HttpWorker(client);
        }

        /// <summary>
        /// Get trivia for random number
        /// </summary>
        /// <exception cref="HttpRequestException"/>
        public async Task<string> RandomTriviaAsync() => await Client.GetResponse("random", "trivia");

        /// <summary>
        /// Get fact about random date
        /// </summary>
        /// <exception cref="HttpRequestException"/>
        public async Task<string> RandomDateAsync() => await Client.GetResponse("random", "date");

        /// <summary>
        /// Get fact about random year
        /// </summary>
        /// <exception cref="HttpRequestException"/>
        public async Task<string> RandomYearAsync() => await Client.GetResponse("random", "year");

        /// <summary>
        /// Get math fact about random number
        /// </summary>
        /// <exception cref="HttpRequestException"/>
        public async Task<string> RandomMathAsync() => await Client.GetResponse("random", "math");

        /// <summary>
        /// Get fact about given number
        /// </summary>
        /// <param name="number">Number for search querry</param>
        /// <exception cref="HttpRequestException"/>
        /// <exception cref="NumbersNotFoundException"/>
        public async Task<string> TriviaAsync(int number) => await Client.GetResponse(number.ToString(), "trivia");

        /// <summary>
        /// Get fact about given date
        /// </summary>
        /// <param name="date">Date for search querry</param>
        /// <exception cref="HttpRequestException"/>
        /// <exception cref="NumbersNotFoundException"/>
        public async Task<string> DateAsync(DateTime date) => await Client.GetResponse($"{date.Month}/{date.Day}", "date");

        /// <summary>
        /// Get fact about given year
        /// </summary>
        /// <param name="year"></param>
        /// <param name="year">Year for search querry</param>
        /// <exception cref="HttpRequestException"/>
        /// <exception cref="NumbersNotFoundException"/>
        public async Task<string> YearAsync(int year) => await Client.GetResponse(year.ToString(), "year");

        /// <summary>
        /// Get math fact about given number
        /// </summary>
        /// <param name="number">Number for search querry</param>
        /// <exception cref="HttpRequestException"/>
        /// <exception cref="NumbersNotFoundException"/>
        public async Task<string> MathAsync(int number) => await Client.GetResponse(number.ToString(), "math");
    }
}
