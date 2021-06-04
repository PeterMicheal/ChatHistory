using System;
using System.Collections.Generic;
using System.Text;
using PowerDiaryDataAccess.Models;

namespace PowerDiaryBusiness.MessagesFormatter
{
    public class Format
    {
        private readonly IEventMessageFormatter _eventMessageFormatter;

        public Format(IEventMessageFormatter eventMessageFormatter)
        {
            _eventMessageFormatter = eventMessageFormatter;
        }

        public string GetDetailedText(Chat chat)
        {
            return _eventMessageFormatter.GetDetailedText(chat);
        }

        public string GetHourlyText(List<Chat> chats)
        {
            return _eventMessageFormatter.GetHourlyText(chats);
        }

    }
}
