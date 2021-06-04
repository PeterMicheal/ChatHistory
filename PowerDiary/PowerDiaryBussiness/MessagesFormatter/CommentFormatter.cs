using System;
using System.Collections.Generic;
using System.Text;
using PowerDiaryDataAccess.Models;

namespace PowerDiaryBusiness.MessagesFormatter
{
    public class CommentFormatter : IEventMessageFormatter
    {
        public string GetDetailedText(Chat chat)
        {
            return $"{chat.User.Name} comments: \"{chat.Message}\"";
        }

        public string GetHourlyText(List<Chat> chats)
        {
            return $"{chats.Count} comments";
        }
    }
}
