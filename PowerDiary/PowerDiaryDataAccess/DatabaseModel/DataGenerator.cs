using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PowerDiaryDataAccess.Models;

namespace PowerDiaryDataAccess.DatabaseModel
{
    public class DataGenerator
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            var options = new DbContextOptionsBuilder<ChatDbContext>()
                .UseInMemoryDatabase(databaseName: "PowerDiary")
                .Options;

            using (var context = new ChatDbContext(options))
            {
                var Users = new List<User>()
                {
                    new User()
                    {
                        Id = 1,
                        Name = "Bob"
                    },
                    new User()
                    {
                        Id = 2,
                        Name = "Kate"
                    },
                    new User()
                    {
                        Id = 3,
                        Name = "Peter"
                    },
                    new User()
                    {
                        Id = 4,
                        Name = "Sam"
                    },
                };

                var chats = new List<Chat>()
                {

                    new Chat()
                    {
                        Id = 1,
                        EventTypeId = EventTypeEnum.Enter, UserId = 1,
                        Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 5, 0, 0),
                    },
                    new Chat()
                    {
                        Id = 2,
                        EventTypeId = EventTypeEnum.Enter, UserId = 2,
                        Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 5, 5, 0),
                    },
                    new Chat()
                    {
                        Id = 3,
                        EventTypeId = EventTypeEnum.Comment, UserId = 1,
                        Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 5, 15, 0),
                        Message = "Hey, Kate - high five?"
                    },
                    new Chat()
                    {
                        Id = 4,
                        EventTypeId = EventTypeEnum.HighFive, UserId = 2, UserToId = 1,
                        Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 5, 17, 0),
                    },
                    new Chat()
                    {
                        Id = 5,
                        EventTypeId = EventTypeEnum.Leave, UserId = 1,
                        Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 5, 18, 0),
                    },
                    new Chat()
                    {
                        Id = 6,
                        EventTypeId = EventTypeEnum.Comment,
                        UserId = 2,
                        Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 5, 20, 0),
                        Message = "Oh, typical"
                    },
                    new Chat()
                    {
                        Id = 7,
                        EventTypeId = EventTypeEnum.Leave,
                        UserId = 2,
                        Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 5, 21, 0),
                    },

                    new Chat()
                    {
                        Id = 8, EventTypeId = EventTypeEnum.Enter, UserId = 1,
                        Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 5, 0),

                    },
                    new Chat()
                    {
                        Id = 9, EventTypeId = EventTypeEnum.Enter, UserId = 2,
                        Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 17, 0),
                    },
                    new Chat()
                    {
                        Id = 10, EventTypeId = EventTypeEnum.Enter, UserId = 3,
                        Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 19, 0),
                    },

                    new Chat()
                    {
                        Id = 11, EventTypeId = EventTypeEnum.HighFive, UserId = 1, UserToId = 2,
                        Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 21, 0),
                    },
                    new Chat()
                    {
                        Id = 12, EventTypeId = EventTypeEnum.HighFive, UserId = 1, UserToId = 3,
                        Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 21, 0),
                    },
                    new Chat()
                    {
                        Id = 13, EventTypeId = EventTypeEnum.HighFive, UserId = 1,UserToId = 4,
                        Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 21, 0)
                    },
                    new Chat()
                    {
                        Id = 14, EventTypeId = EventTypeEnum.Comment, UserId = 2,
                        Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 27, 0),
                        Message = "Message 1"
                    },
                    new Chat()
                    {
                        Id = 15, EventTypeId = EventTypeEnum.Comment, UserId = 3,
                        Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 28, 0),
                        Message = "Message 2"
                    },
                    new Chat()
                    {
                        Id = 16, EventTypeId = EventTypeEnum.Comment, UserId = 2,
                        Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 30, 0),
                        Message = "Message 3"
                    },
                    new Chat()
                    {
                        Id = 17, EventTypeId = EventTypeEnum.Comment, UserId = 3,
                        Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 32, 0),
                        Message = "Message 4"
                    },
                    new Chat()
                    {
                        Id = 18, EventTypeId = EventTypeEnum.Comment, UserId = 2,
                        Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 35, 0),
                        Message = "Message 5"
                    },
                    new Chat()
                    {
                        Id = 19, EventTypeId = EventTypeEnum.Comment, UserId = 3,
                        Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 39, 0),
                        Message = "Message 6"
                    },


                };

                context.User.AddRange(Users);
                context.Chat.AddRange(chats);
                context.SaveChanges();
            }
        }
    }
}
