using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Tests.Services
{
    [TestClass]
    public class GiftServiceTests
    {
        Gift CreateGift()
        {
            var user = new User
            {
                FirstName = "Inigo",
                LastName = "Montoya"
            };

            var gift = new Gift
            {
                Title = "My second gift",
                OrderOfImportance = 2,
                Url = "www.abc.com",
                Description = "My second description",
                User = user
            };

            return gift;
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
                GiftService service = new GiftService(context);
                var myGift = CreateGift();

                var persistedGift = service.AddGift(myGift);

                Assert.AreNotEqual(0, persistedGift.Id);
            }
        }
        [TestMethod]
        public void FindGift()
        {
            GiftService giftService;
            Gift gift = CreateGift();

            using (ApplicationDbContext context = new ApplicationDbContext(Options))
            {
                giftService = new GiftService(context);

                giftService.AddGift(gift);
            }

            using (ApplicationDbContext context = new ApplicationDbContext(Options))
            {
                giftService = new GiftService(context);

                Gift foundGift = giftService.Find(1);

                Assert.AreEqual("My second description", foundGift.Description);
                Assert.AreEqual(2, foundGift.OrderOfImportance);
            }
        }

        [TestMethod]
        public void UpdateGift()
        {
            GiftService giftService;
            Gift gift = CreateGift();

            using (ApplicationDbContext context = new ApplicationDbContext(Options))
            {
                giftService = new GiftService(context);

                giftService.AddGift(gift);
            }

            using (ApplicationDbContext context = new ApplicationDbContext(Options))
            {
                giftService = new GiftService(context);

                Gift userGift = giftService.Find(1);

                userGift.Description = "The updated description";
                userGift.OrderOfImportance = 1;
                giftService.UpdateGift(userGift);
            }

            using (ApplicationDbContext context = new ApplicationDbContext(Options))
            {
                giftService = new GiftService(context);
                Gift userGift = giftService.Find(1);

                Assert.AreEqual("The updated description", userGift.Description);
                Assert.AreEqual(1, userGift.OrderOfImportance);
            }
        }

        [TestMethod]
        public void RemoveGift()
        {
            GiftService giftService;
            Gift gift = CreateGift();

            using (ApplicationDbContext context = new ApplicationDbContext(Options))
            {
                giftService = new GiftService(context);

                giftService.AddGift(gift);
            }

            using (ApplicationDbContext context = new ApplicationDbContext(Options))
            {
                giftService = new GiftService(context);

                giftService.RemoveGift(gift);

                Assert.IsNull(giftService.Find(gift.Id));
            }
        }


    }
}
