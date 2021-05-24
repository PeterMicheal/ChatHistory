using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerDiaryBusiness;
using PowerDiaryDataAccess.DataAccess;
using PowerDiaryDataAccess.DatabaseModel;

namespace PowerDiaryUnitTest
{
    [TestClass]
    public class UnitTest
    {
        private static IPowerDiaryBusiness powerDiaryBusiness;

        /// <summary>
        /// TestClassInitialize for intializing services
        /// </summary>
        /// <param name="TestContext"></param>
        [ClassInitialize]
        public static void TestClassInitialize(TestContext TestContext)
        {
            var services = new ServiceCollection();

            services.AddSingleton<IPowerDiaryBusiness, PowerDiaryBusiness.PowerDiaryBusiness>();
            services.AddSingleton<IChatRepository, ChatRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();

            services.AddSingleton(option =>
            {
                var contextOptions = new DbContextOptionsBuilder<PowerDiaryDbContext>()
                    .UseInMemoryDatabase(databaseName: "PowerDiary")
                    .Options;

                return new PowerDiaryDbContext(contextOptions);
            });

            var serviceProvider = services.BuildServiceProvider();

            DataGenerator.Initialize(serviceProvider);

            var chatRepository = serviceProvider.GetService<IChatRepository>();
            var userRepository = serviceProvider.GetService<IUserRepository>(); 
            powerDiaryBusiness = serviceProvider.GetService<IPowerDiaryBusiness>();
        }

        /// <summary>
        /// checking today Chat data count
        /// </summary>
        [TestMethod]
        public void GetDetailsViewTest()
        {
            var chatDetailedView = powerDiaryBusiness.GetChatDetailedView(DateTime.Now);
            Assert.AreEqual(chatDetailedView.Status, ServiceResponseDtoStatus.Success);
            Assert.AreEqual(19, chatDetailedView.Data.Count);
        }


        /// <summary>
        /// checking today aggregated Chat data count
        /// </summary>
        [TestMethod]
        public void GetHourlyViewTest()
        {
            var chatHourlyView = powerDiaryBusiness.GetChatsHourView(DateTime.Now);
            Assert.AreEqual(chatHourlyView.Status, ServiceResponseDtoStatus.Success);
            Assert.AreEqual(2, chatHourlyView.Data.Count);
            Assert.AreEqual(4, chatHourlyView.Data[0].ChatEvents.Count);
        }
    }
}
