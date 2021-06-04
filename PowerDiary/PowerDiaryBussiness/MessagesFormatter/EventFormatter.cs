using System;
using System.Collections.Generic;
using System.Text;
using PowerDiaryDataAccess.Models;

namespace PowerDiaryBusiness.MessagesFormatter
{
    public class EventFormatter : IEventFormatter
    {
        public IEventMessageFormatter CreateEventMessageFormatter(EventTypeEnum eventType)
        {
            switch (eventType)
            {
                case EventTypeEnum.Enter: return new EnteredFormatter();
                case EventTypeEnum.Leave: return new LeaveFormatter();
                case EventTypeEnum.Comment: return new CommentFormatter();
                case EventTypeEnum.HighFive: return new HighFiveFormatter();
                default: throw new ArgumentException("Invalid type", "type");
            }
        }
    }
}
