using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace NumbersAPI.NET.Models
{
    internal class NumbersResponse
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("found")]
        public bool Found { get; set; }

        [JsonPropertyName("number")]
        public int Number { get; set; }

        [JsonPropertyName("year")]
        public string Type { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }
    }
}
