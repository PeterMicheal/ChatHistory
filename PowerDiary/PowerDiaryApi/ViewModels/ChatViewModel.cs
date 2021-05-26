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
        /// <summary>
        /// Id of Message 
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Message body
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// Time Of Message
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Time { get; set; }
    }
}
