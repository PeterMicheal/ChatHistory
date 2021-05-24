using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace PowerDiaryApi.ViewModels
{
    [DataContract()]
    public class HourlyChatViewModel
    {

        public HourlyChatViewModel()
        {
            ChatEvents = new List<string>();
        }

        /// <summary>
        /// Time Of Message
        /// </summary>
        [DataMember(Name = "time")]
        public DateTime Time { get; set; }

        /// <summary>
        /// List of Aggregated Events
        /// </summary>
        [DataMember(Name = "chatEvents")]
        public List<string> ChatEvents { get; set; }
    }
}
