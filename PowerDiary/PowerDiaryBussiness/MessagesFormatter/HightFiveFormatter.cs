using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PowerDiaryDataAccess.Models;

namespace PowerDiaryBusiness.MessagesFormatter
{
    public class HighFiveFormatter : IEventMessageFormatterInterface
    {
        public string GetDetailedText(Chat chat)
        {
            return $"{chat.User.Name} high-fives { chat.UserTo.Name }";
        }

        public string GetHourlyText(List<Chat> chats)
        {
            var personFromValue = chats.Select(x => x.UserId).Distinct().Count() == 1 ? "person" : "people";
            var personToValue = chats.Distinct().Count(c => c.UserToId != null) == 1 ? "person" : "people";

            return $"{ chats.Select(x => x.UserId).Distinct().Count() } { personFromValue } high - fived { chats.Distinct().Count(c => c.UserToId != null) } other { personToValue }";
        }
    }
}
