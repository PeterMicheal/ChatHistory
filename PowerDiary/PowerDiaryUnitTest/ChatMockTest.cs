using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PowerDiaryBusiness;
using PowerDiaryBusiness.MessagesFormatter;
using PowerDiaryDataAccess.DataAccess;
using PowerDiaryDataAccess.Models;

namespace PowerDiaryUnitTest
{
    [TestClass]
    public class ChatMockTest
    {
        [TestMethod]
        public void GetDetailsViewMockTest()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var chatRepositoryMock = new Mock<IChatRepository>();

            var eventFormatterMock = new Mock<IEventFormatter>();
            eventFormatterMock.Setup(m => m.CreateEventMessageFormatter(EventTypeEnum.Enter))
                .Returns(new EnteredFormatter());
            eventFormatterMock.Setup(m => m.CreateEventMessageFormatter(EventTypeEnum.Leave))
                .Returns(new LeaveFormatter());
            eventFormatterMock.Setup(m => m.CreateEventMessageFormatter(EventTypeEnum.Comment))
                .Returns(new CommentFormatter());
            eventFormatterMock.Setup(m => m.CreateEventMessageFormatter(EventTypeEnum.HighFive))
                .Returns(new HighFiveFormatter());

            chatRepositoryMock.Setup(p => p.GetAll()).
                Returns(GetChats());

            var powerDiaryBusiness = new PowerDiaryBusiness.ChatBusiness(chatRepositoryMock.Object, userRepositoryMock.Object, eventFormatterMock.Object);
            var chatDetailedView = powerDiaryBusiness.GetChatDetailedView(DateTime.Now);
            Assert.AreEqual(chatDetailedView.Status, ServiceResponseDtoStatus.Success);
            Assert.AreEqual(7, chatDetailedView.Data.Count);
        }

        [TestMethod]
        public void GetHourlyViewMockTest()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var chatRepositoryMock = new Mock<IChatRepository>();

            var eventFormatterMock = new Mock<IEventFormatter>();
            eventFormatterMock.Setup(m => m.CreateEventMessageFormatter(EventTypeEnum.Enter))
                .Returns(new EnteredFormatter());
            eventFormatterMock.Setup(m => m.CreateEventMessageFormatter(EventTypeEnum.Leave))
                .Returns(new LeaveFormatter());
            eventFormatterMock.Setup(m => m.CreateEventMessageFormatter(EventTypeEnum.Comment))
                .Returns(new CommentFormatter());
            eventFormatterMock.Setup(m => m.CreateEventMessageFormatter(EventTypeEnum.HighFive))
                .Returns(new HighFiveFormatter());

            chatRepositoryMock.Setup(p => p.GetAll()).
                Returns(GetChats());

            var powerDiaryBusiness = new PowerDiaryBusiness.ChatBusiness(chatRepositoryMock.Object, userRepositoryMock.Object, eventFormatterMock.Object);
            var chatHourlyView = powerDiaryBusiness.GetChatsHourView(DateTime.Now);

            Assert.AreEqual(chatHourlyView.Status, ServiceResponseDtoStatus.Success);
            Assert.AreEqual(1, chatHourlyView.Data.Count);
            Assert.AreEqual(4, chatHourlyView.Data[0].ChatEvents.Count);
        }

        private IQueryable<Chat> GetChats()
        {
            var chats = new List<Chat>()
            {
                new Chat()
                {
                    Id = 1,
                    EventTypeId = EventTypeEnum.Enter, 
                    UserId = 1, User = new User() { Id = 1 , Name = "Bob"},
                    Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 5, 0, 0),
                },
                new Chat()
                {
                    Id = 2,
                    EventTypeId = EventTypeEnum.Enter, 
                    UserId = 2, User = new User() { Id = 2 , Name = "Kate"},
                    Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 5, 5, 0),
                },
                new Chat()
                {
                    Id = 3,
                    EventTypeId = EventTypeEnum.Comment, 
                    UserId = 1, User = new User() { Id = 1 , Name = "Bob"},
                    Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 5, 15, 0),
                    Message = "Hey, Kate - high five?",
                    
                },
                new Chat()
                {
                    Id = 4,
                    EventTypeId = EventTypeEnum.HighFive, 
                    UserId = 2, User = new User() { Id = 2 , Name = "Kate"},
                    UserToId = 1, UserTo = new User() { Id = 1 , Name = "Bob"},
                    Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 5, 17, 0)
                },
                new Chat()
                {
                    Id = 5,
                    EventTypeId = EventTypeEnum.Leave, 
                    UserId = 1, User = new User() { Id = 1 , Name = "Bob"},
                    Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 5, 18, 0),
                },
                new Chat()
                {
                    Id = 6,
                    EventTypeId = EventTypeEnum.Comment,
                    UserId = 2, User = new User() { Id = 2 , Name = "Kate"},
                    Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 5, 20, 0),
                    Message = "Oh, typical"
                },
                new Chat()
                {
                    Id = 7,
                    EventTypeId = EventTypeEnum.Leave,
                    UserId = 2, User = new User() { Id = 2 , Name = "Kate"},
                    Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 5, 21, 0),
                },
            };
            return chats.AsQueryable();
        }
    }
}
