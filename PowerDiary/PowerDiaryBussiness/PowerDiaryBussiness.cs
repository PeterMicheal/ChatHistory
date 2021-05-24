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
    public class PowerDiaryBusiness : IPowerDiaryBusiness
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;

        public PowerDiaryBusiness(IChatRepository chatRepository, IUserRepository userRepository) 
        {
            _chatRepository = chatRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Get Chat Detailed View model for selected date 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
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
                        Message = $"{ GetDetailedMessage(c) }"
                    });
                    chatMessagesVm = chatMessages.ToList();
                    retVal = SR.Successfull<List<VrChat>>(chatMessagesVm);
            }
            catch (Exception ex)
            {
                retVal = SR.Failed<List<VrChat>>(ex, "GetChatDetailedView");
            }
                
            return retVal;
        }

        /// <summary>
        /// Get Chats Hour View model for selected date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public ServiceResponseDto<List<VrHourlyChat>> GetChatsHourView(DateTime date)
        {
            ServiceResponseDto<List<VrHourlyChat>> retVal;
            var hourlyChatList = new List<VrHourlyChat>();
            try
            {
                var chats = GetChatOrderedAndFilterByDate(date);

                var chatGroupedByHour = chats.AsEnumerable().GroupBy(x => x.Time.Hour);
                
                // for each every hour to get actions of hour 
                foreach (var chatHour in chatGroupedByHour)
                {
                    var hourlyChat = new VrHourlyChat();
                    hourlyChat.Time = new DateTime(chatHour.First().Time.Year, chatHour.First().Time.Month, chatHour.First().Time.Day, chatHour.First().Time.Hour, 0, 0);

                    var eventsChatHour = chatHour.GroupBy(x => x.EventTypeId);

                    foreach (var eventChatHour in eventsChatHour)
                    {
                        var eventDetails = $"{  GetHourlyMessage(eventChatHour.ToList())  }";
                        hourlyChat.ChatEvents.Add(eventDetails);
                    }

                    hourlyChatList.Add(hourlyChat);
                }

                retVal = SR.Successfull<List<VrHourlyChat>>(hourlyChatList);
            }
            catch (Exception ex)
            {
                retVal = SR.Failed<List<VrHourlyChat>>(ex, "GetChatsHourView");
            }

            return retVal;
        }

        /// <summary>
        /// Get Chats for specific day and order by time  
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private IOrderedQueryable<Chat> GetChatOrderedAndFilterByDate(DateTime date)
        {
            var chats = _chatRepository.GetAll();

            var startDate = new DateTime(date.Year, date.Month, date.Day);
            var endDate = new DateTime(date.Year, date.Month, date.Day).AddDays(1).AddSeconds(-1);
            var chatsWithConditionAndOrder = chats.Where(s => s.Time >= startDate && s.Time <= endDate)
                .OrderBy(x => x.Time);

            return chatsWithConditionAndOrder;
        }

        /// <summary>
        /// Get Formatted message for Detailed view
        /// </summary>
        /// <param name="chat"></param>
        /// <returns></returns>
        private static string GetDetailedMessage(Chat chat)
        {
            switch (chat.EventTypeId)
            {
                case EventTypeEnum.Enter: return new Format(new EnteredFormatter()).GetDetailedText(chat);
                case EventTypeEnum.Leave: return new Format(new LeaveFormatter()).GetDetailedText(chat);
                case EventTypeEnum.Comment: return new Format(new CommentFormatter()).GetDetailedText(chat);
                case EventTypeEnum.HighFive: return new Format(new HighFiveFormatter()).GetDetailedText(chat);
                default: throw new ArgumentException("Invalid type", "type");
            }
        }

        /// <summary>
        /// Get Formatted message for Hourly view
        /// </summary>
        /// <param name="chats"></param>
        /// <returns></returns>
        private string GetHourlyMessage(List<Chat> chats)
        {
            switch (chats.First().EventTypeId)
            {
                case EventTypeEnum.Enter: return new Format(new EnteredFormatter()).GetHourlyText(chats);
                case EventTypeEnum.Leave: return new Format(new LeaveFormatter()).GetHourlyText(chats);
                case EventTypeEnum.Comment: return new Format(new CommentFormatter()).GetHourlyText(chats);
                case EventTypeEnum.HighFive: return new Format(new HighFiveFormatter()).GetHourlyText(chats);
                default: throw new ArgumentException("Invalid type", "type");
            }
        }


    }
}
