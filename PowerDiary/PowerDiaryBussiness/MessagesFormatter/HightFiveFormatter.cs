using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PowerDiaryDataAccess.Models;

namespace PowerDiaryBusiness.MessagesFormatter
{
    public class HighFiveFormatter : IEventMessageFormatter
    {
        public string GetDetailedText(Chat chat)
        {
            return $"{chat.User.Name} high-fives { chat.UserTo.Name }";
        }

        public string GetHourlyText(List<Chat> chats)
        {
            var personCount = chats.Select(x => x.UserId).Distinct().Count();
            var personToCount = chats.Distinct().Count(c => c.UserToId != null);
            var personFromValue = personCount == 1 ? "person" : "people";
            var personToValue = personToCount == 1 ? "person" : "people";

            return $"{ personCount } { personFromValue } high - fived { personToCount } other { personToValue }";
        }
    }
}
