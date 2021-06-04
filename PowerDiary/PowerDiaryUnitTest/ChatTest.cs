using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerDiaryBusiness;
using PowerDiaryBusiness.MessagesFormatter;
using PowerDiaryDataAccess.DataAccess;
using PowerDiaryDataAccess.DatabaseModel;
using PowerDiaryDataAccess.Models;

namespace PowerDiaryUnitTest
{
    [TestClass]
    public class UnitTest
    {
        private static IChatBusiness _powerDiaryBusiness;

        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            var services = new ServiceCollection();
            services.AddTransient<IEventFormatter, EventFormatter>();
            services.AddSingleton<IChatBusiness, PowerDiaryBusiness.ChatBusiness>();
            services.AddSingleton<IChatRepository, ChatRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();

            services.AddSingleton(option =>
            {
                var contextOptions = new DbContextOptionsBuilder<ChatDbContext>()
                    .UseInMemoryDatabase(databaseName: "PowerDiary")
                    .Options;

                return new ChatDbContext(contextOptions);
            });

            var serviceProvider = services.BuildServiceProvider();

            DataGenerator.Initialize(serviceProvider);

            var chatRepository = serviceProvider.GetService<IChatRepository>();
            var userRepository = serviceProvider.GetService<IUserRepository>(); 
            _powerDiaryBusiness = serviceProvider.GetService<IChatBusiness>();
        }

        [TestMethod]
        public void GetDetailsViewTest()
        {
            var chatDetailedView = _powerDiaryBusiness.GetChatDetailedView(DateTime.Now);
            Assert.AreEqual(chatDetailedView.Status, ServiceResponseDtoStatus.Success);
            Assert.AreEqual(19, chatDetailedView.Data.Count);
        }

        [TestMethod]
        public void GetHourlyViewTest()
        {
            var chatHourlyView = _powerDiaryBusiness.GetChatsHourView(DateTime.Now);
            Assert.AreEqual(chatHourlyView.Status, ServiceResponseDtoStatus.Success);
            Assert.AreEqual(2, chatHourlyView.Data.Count);
            Assert.AreEqual(4, chatHourlyView.Data[0].ChatEvents.Count);
        }

        [TestMethod]
        public void GetDetailedChatMessages()
        {
            var chat = new Chat()
            {
                User = new User() { Name = "Bob" },
                Message = "Hey, Kate - high five?",
                UserTo = new User() { Name = "Kate" }
            };

            var enterFormattedMessage = new Format(new EnteredFormatter()).GetDetailedText(chat);
            Assert.AreEqual("Bob enters the room", enterFormattedMessage);

            var commentFormattedMessage = new Format(new CommentFormatter()).GetDetailedText(chat);
            Assert.AreEqual("Bob comments: \"Hey, Kate - high five?\"", commentFormattedMessage);

            var highFiveFormattedMessage = new Format(new HighFiveFormatter()).GetDetailedText(chat);
            Assert.AreEqual("Bob high-fives Kate", highFiveFormattedMessage);

            var leaveFormattedMessage = new Format(new LeaveFormatter()).GetDetailedText(chat);
            Assert.AreEqual("Bob leaves", leaveFormattedMessage);
        }

        [TestMethod]
        public void GetHourlyChatMessages()
        {
            var chats = new List<Chat>()
            {
                new Chat()
                {
                    EventTypeId = EventTypeEnum.Enter, UserId = 1,
                },
                new Chat()
                {
                    EventTypeId = EventTypeEnum.Enter, UserId = 2,
                },
                new Chat()
                {
                    EventTypeId = EventTypeEnum.Comment, UserId = 1,
                    Message = "Hey, Kate - high five?"
                },
                new Chat()
                {
                    EventTypeId = EventTypeEnum.HighFive, UserId = 2, UserToId = 1,
                },
                new Chat()
                {
                    EventTypeId = EventTypeEnum.Leave, UserId = 1,
                },
                new Chat()
                {
                    EventTypeId = EventTypeEnum.Comment, UserId = 2,
                    Message = "Oh, typical"
                },
                new Chat()
                {
                    EventTypeId = EventTypeEnum.Leave, UserId = 2,
                },
            };

            var enterGroupedChats = chats.Where(x => x.EventTypeId == EventTypeEnum.Enter).ToList();
            var enterFormattedMessage = new Format(new EnteredFormatter()).GetHourlyText(enterGroupedChats);
            Assert.AreEqual("2 people entered", enterFormattedMessage);

            var commentGroupedChats = chats.Where(x => x.EventTypeId == EventTypeEnum.Comment).ToList();
            var commentFormattedMessage = new Format(new CommentFormatter()).GetHourlyText(commentGroupedChats);
            Assert.AreEqual("2 comments", commentFormattedMessage);

            var highFiveGroupedChats = chats.Where(x => x.EventTypeId == EventTypeEnum.HighFive).ToList();
            var highFiveFormattedMessage = new Format(new HighFiveFormatter()).GetHourlyText(highFiveGroupedChats);
            Assert.AreEqual("1 person high - fived 1 other person", highFiveFormattedMessage);

            var leaveGroupedChats = chats.Where(x => x.EventTypeId == EventTypeEnum.Comment).ToList();
            var leaveFormattedMessage = new Format(new LeaveFormatter()).GetHourlyText(leaveGroupedChats);
            Assert.AreEqual("2 left", leaveFormattedMessage);
        }
    }
}
