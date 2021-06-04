using System;
using System.Collections.Generic;
using System.Text;
using PowerDiaryDataAccess.Models;

namespace PowerDiaryBusiness.MessagesFormatter
{
    public class LeaveFormatter : IEventMessageFormatter
    {
        public string GetDetailedText(Chat chat)
        {
            return $"{chat.User.Name} leaves";
        }

        public string GetHourlyText(List<Chat> chats)
        {
            return $"{ chats.Count } left";
        }
    }
}
