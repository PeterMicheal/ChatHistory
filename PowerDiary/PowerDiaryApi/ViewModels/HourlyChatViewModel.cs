using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PowerDiaryApi.ViewModels
{
    public class HourlyChatViewModel
    {

        public HourlyChatViewModel()
        {
            ChatEvents = new List<string>();
        }

        /// <summary>
        /// Time Of Message
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Time { get; set; }

        /// <summary>
        /// List of Aggregated Events
        /// </summary>
        [JsonPropertyName("chatEvents")]
        public List<string> ChatEvents { get; set; }
    }
}
