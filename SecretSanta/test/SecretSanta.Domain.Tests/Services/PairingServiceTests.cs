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
    public class PairingServiceTests
    {
        Pairing CreatePairing()
        {
            var santa = new User
            {
                FirstName = "Richard",
                LastName = "Teller"
            };

            var recipient = new User
            {
                FirstName = "Tuan",
                LastName = "Dang"
            };

            var pairing = new Pairing
            {
                Santa = santa,
                Recipient = recipient
            };

            return pairing;
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
        public void AddPairing()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                PairingService service = new PairingService(context);

                var myPairing = CreatePairing();

                var persistedPairing = service.AddPairing(myPairing);

                Assert.AreNotEqual(0, persistedPairing.Id);
            }
        }
    }
}
