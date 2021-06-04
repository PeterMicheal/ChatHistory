using System;
using System.Collections.Generic;
using System.Text;
using PowerDiaryDataAccess.Models;

namespace PowerDiaryBusiness.MessagesFormatter
{
    public interface IEventMessageFormatter
    {
        string GetDetailedText(Chat chat);

        string GetHourlyText(List<Chat> chats);
    }
}
