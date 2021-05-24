using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace PowerDiaryApi.ViewModels
{
    [DataContract()]
    public class ChatViewModel
    {
        /// <summary>
        /// Id of Message 
        /// </summary>
        [DataMember(Name = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Message body
        /// </summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }

        /// <summary>
        /// Time Of Message
        /// </summary>
        [DataMember(Name = "time")]
        public DateTime Time { get; set; }
    }
}
