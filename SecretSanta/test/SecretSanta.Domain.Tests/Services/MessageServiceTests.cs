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
    public class MessageServiceTests
    {
        Message CreateMessage()
        {
            var toUser = new User
            {
                FirstName = "Richard",
                LastName = "Teller"
            };

            var fromUser = new User
            {
                FirstName = "Tuan",
                LastName = "Dang"
            };

            var message = new Message
            {
                FromUser = fromUser,
                ToUser = toUser,
                MessageContent = "How are you doing today"
        
            };
            
            return message;
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
        public void AddMessage()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                MessageService service = new MessageService(context);
                
                var myMessage = CreateMessage();

                var persistedMessage = service.AddMessage(myMessage);

                Assert.AreNotEqual(0, persistedMessage.Id);
            }
        }

    }
}
