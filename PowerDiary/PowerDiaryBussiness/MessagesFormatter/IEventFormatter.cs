using System;
using System.Collections.Generic;
using System.Text;
using PowerDiaryDataAccess.Models;

namespace PowerDiaryBusiness.MessagesFormatter
{
    public interface IEventFormatter
    {
        IEventMessageFormatter CreateEventMessageFormatter(EventTypeEnum eventType);
    }
}
