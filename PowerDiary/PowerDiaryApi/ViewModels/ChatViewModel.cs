using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PowerDiaryApi.ViewModels
{
    public class ChatViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("time")]
        public DateTime Time { get; set; }
    }
}
