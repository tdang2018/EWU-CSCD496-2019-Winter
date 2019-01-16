
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
    public class UserServiceTests
    {
        User CreateUser()
        {
            var user = new User
            {
                FirstName = "Richard",
                LastName = "Teller"
            };

            var gift= new Gift
            {
                Title = "My First Gift",
                OrderOfImportance = 1,
                URL = "www.ewu.edu",
                Description = "My first description",
                User = user,

            };

            user.Gifts = new List<Gift>();
            user.Gifts.Add(gift);

            return user;
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
        public void AddUser()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                UserService service = new UserService(context);
                var myUser = CreateUser();

                var persistedUser = service.AddUser(myUser);

                Assert.AreNotEqual(0, persistedUser.Id);
            }
        }

        [TestMethod]
        public void UpdateUser()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                UserService service = new UserService(context);
                var myUser = CreateUser();

                var persistedUser = service.UpdateUser(myUser);

                Assert.AreNotEqual(0, persistedUser.Id);
            }
        }


    }
}
