using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

namespace SecretSanta.Domain.Tests.Services
{
    [TestClass]
    public class GiftServiceTests
    {
        ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
            {
                builder.AddConsole()
                    .AddFilter(DbLoggerCategory.Database.Command.Name,
                               LogLevel.Information);
            });
            return serviceCollection.BuildServiceProvider().
            GetService<ILoggerFactory>();
        }

        private SqliteConnection SqliteConnection { get; set; }
        private DbContextOptions<ApplicationDbContext> Options { get; set; }

        [TestInitialize]
        public void OpenConnection()
        {
            SqliteConnection = new SqliteConnection("DataSource=:memory:");
            SqliteConnection.Open();

            Options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(SqliteConnection)
                .UseLoggerFactory(GetLoggerFactory())
                .EnableSensitiveDataLogging()
                .Options;

            using (var context = new ApplicationDbContext(Options))
            {
                context.Database.EnsureCreated();
            }
        }

        [TestCleanup]
        public void CloseConnection()
        {
            SqliteConnection.Close();
        }

        [TestMethod]
        public void AddGift()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                GiftService giftService = new GiftService(context);
                UserService userService = new UserService(context);

                var user = new User
                {
                    FirstName = "Inigo",
                    LastName = "Montoya"
                };

                user = userService.AddUser(user);

                var gift = new Gift
                {
                    Title = "Sword",
                    OrderOfImportance = 1
                };

                var persistedGift = giftService.AddGiftToUser(user.Id, gift);

                Assert.AreNotEqual(0, persistedGift.Id);
            }
        }

        [TestMethod]
        public void UpdateGift()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                GiftService giftService = new GiftService(context);
                UserService userService = new UserService(context);

                var user = new User
                {
                    FirstName = "Inigo",
                    LastName = "Montoya"
                };

                user = userService.AddUser(user);

                var gift = new Gift
                {
                    Title = "Sword",
                    OrderOfImportance = 1
                };

                var persistedGift = giftService.AddGiftToUser(user.Id, gift);

                Assert.AreNotEqual(0, persistedGift.Id);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                GiftService giftService = new GiftService(context);
                UserService userService = new UserService(context);

                var users = userService.FetchAll();
                var gifts = giftService.GetGiftsForUser(users[0].Id);

                Assert.IsTrue(gifts.Count > 0);

                gifts[0].Title = "Horse";
                giftService.UpdateGiftForUser(users[0].Id, gifts[0]);                
            }

            using (var context = new ApplicationDbContext(Options))
            {
                GiftService giftService = new GiftService(context);
                UserService userService = new UserService(context);

                var users = userService.FetchAll();
                var gifts = giftService.GetGiftsForUser(users[0].Id);

                Assert.IsTrue(gifts.Count > 0);
                Assert.AreEqual("Horse", gifts[0].Title);            
            }
        }
    }
}