using System;
using System.Collections.Generic;
using System.Text;

namespace PowerDiaryBusiness.BusinessViewModels
{
    public class VrHourlyChat
    {
        public VrHourlyChat()
        {
            ChatEvents = new List<string>();
        }

        /// <summary>
        /// Time Of Message
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// List of Aggregated Events
        /// </summary>
        public List<string> ChatEvents { get; set; }
    }
}
