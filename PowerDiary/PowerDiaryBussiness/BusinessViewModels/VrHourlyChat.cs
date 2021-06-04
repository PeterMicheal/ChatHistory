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

        public DateTime Time { get; set; }

        public List<string> ChatEvents { get; set; }
    }
}
