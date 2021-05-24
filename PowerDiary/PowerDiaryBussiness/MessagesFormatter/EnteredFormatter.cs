using System;
using System.Collections.Generic;
using System.Text;
using PowerDiaryDataAccess.Models;

namespace PowerDiaryBusiness.MessagesFormatter
{
    public class EnteredFormatter : IEventMessageFormatterInterface
    {
        public string GetDetailedText(Chat chat)
        {
            return $"{chat.User.Name} enters the room";
        }

        public string GetHourlyText(List<Chat> chats)
        {
            return chats.Count == 1 ? $"{chats.Count} person entered" : $"{chats.Count} people entered";
        }
    }
}
