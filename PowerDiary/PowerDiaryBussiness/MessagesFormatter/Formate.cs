using System;
using System.Collections.Generic;
using System.Text;
using PowerDiaryDataAccess.Models;

namespace PowerDiaryBusiness.MessagesFormatter
{
    public class Format
    {
        private IEventMessageFormatterInterface _eventMessageFormatterInterface;

        //Constructor: assigns strategy to interface  
        public Format(IEventMessageFormatterInterface eventMessageFormatter)
        {
            _eventMessageFormatterInterface = eventMessageFormatter;
        }

        //Executes the strategy  
        public string GetDetailedText(Chat chat)
        {
            return _eventMessageFormatterInterface.GetDetailedText(chat);
        }

        //Executes the strategy 
        public string GetHourlyText(List<Chat> chats)
        {
            return _eventMessageFormatterInterface.GetHourlyText(chats);
        }

    }
}
