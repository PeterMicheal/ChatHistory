using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PowerDiaryBusiness.BusinessViewModels;
using PowerDiaryBusiness.MessagesFormatter;
using PowerDiaryDataAccess.DataAccess;
using PowerDiaryDataAccess.Models;

namespace PowerDiaryBusiness
{
    public class ChatBusiness : IChatBusiness
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEventFormatter _eventFormatter;

        public ChatBusiness(IChatRepository chatRepository, IUserRepository userRepository, IEventFormatter eventFormatter) 
        {
            _chatRepository = chatRepository;
            _userRepository = userRepository;
            _eventFormatter = eventFormatter;
        }

        public ServiceResponseDto<List<VrChat>> GetChatDetailedView(DateTime date)
        {
            ServiceResponseDto<List<VrChat>> retVal = null;

            try{
                List<VrChat> chatMessagesVm = new List<VrChat>();

                var chatsWithInclude = GetChatOrderedAndFilterByDate(date)
                    .Include(s => s.User)
                    .Include(s => s.UserTo);

                var chatMessages = chatsWithInclude.
                    Select(c => new VrChat()
                    {
                        Id = c.Id, Time = c.Time,

                        // Message = _eventFormatter.CreateEventMessageFormatter(c.EventTypeId).GetDetailedText(c)
                        Message = new Format(_eventFormatter.CreateEventMessageFormatter(c.EventTypeId)).GetDetailedText(c)
                    });
                    chatMessagesVm = chatMessages.ToList();
                    retVal = ServiceResponse.Successful<List<VrChat>>(chatMessagesVm);
            }
            catch (Exception ex)
            {
                retVal = ServiceResponse.Failed<List<VrChat>>(ex, "GetChatDetailedView");
            }
                
            return retVal;
        }

        public ServiceResponseDto<List<VrHourlyChat>> GetChatsHourView(DateTime date)
        {
            ServiceResponseDto<List<VrHourlyChat>> retVal;
            var hourlyChatList = new List<VrHourlyChat>();
            try
            {
                var chats = GetChatOrderedAndFilterByDate(date);

                var chatGroupedByHour = chats.AsEnumerable().GroupBy(x => x.Time.Hour);
                
                foreach (var chatHour in chatGroupedByHour)
                {
                    var hourlyChat = new VrHourlyChat();
                    hourlyChat.Time = new DateTime(chatHour.First().Time.Year, chatHour.First().Time.Month, chatHour.First().Time.Day, chatHour.First().Time.Hour, 0, 0);

                    var eventsChatHour = chatHour.GroupBy(x => x.EventTypeId);

                    foreach (var eventChatHour in eventsChatHour)
                    {
                        //var eventDetails = _eventFormatter.CreateEventMessageFormatter(eventChatHour.Key)
                        //    .GetHourlyText(eventChatHour.ToList());

                        var eventDetails = new Format(_eventFormatter.CreateEventMessageFormatter(eventChatHour.Key))
                            .GetHourlyText(eventChatHour.ToList());

                        hourlyChat.ChatEvents.Add(eventDetails);
                    }

                    hourlyChatList.Add(hourlyChat);
                }

                retVal = ServiceResponse.Successful<List<VrHourlyChat>>(hourlyChatList);
            }
            catch (Exception ex)
            {
                retVal = ServiceResponse.Failed<List<VrHourlyChat>>(ex, "GetChatsHourView");
            }

            return retVal;
        }

        private IOrderedQueryable<Chat> GetChatOrderedAndFilterByDate(DateTime date)
        {
            var chats = _chatRepository.GetAll();

            var startDate = new DateTime(date.Year, date.Month, date.Day);
            var endDate = new DateTime(date.Year, date.Month, date.Day).AddDays(1).AddSeconds(-1);
            var chatsWithConditionAndOrder = chats.Where(s => s.Time >= startDate && s.Time <= endDate)
                .OrderBy(x => x.Time);

            return chatsWithConditionAndOrder;
        }
    }
}
