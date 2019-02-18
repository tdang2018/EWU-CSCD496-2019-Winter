using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Domain.Tests.Services
{
    [TestClass]
    public class PairingServiceTests:DatabaseServiceTests
    {
        [TestInitialize]
        public async Task Initialize()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var groupService = new GroupService(context);
                var userService = new UserService(context);

                var user = new User
                {
                    FirstName = "John",
                    LastName = "Cena"
                };
                var user2 = new User
                {
                    FirstName = "Randy",
                    LastName = "Orton"
                };
                var user3 = new User
                {
                    FirstName = "Michael",
                    LastName = "Jackson"
                };
                var user4 = new User
                {
                    FirstName = "Nikki",
                    LastName = "Bella"
                };

                user = await userService.AddUser(user);
                user2 = await userService.AddUser(user2);
                user3 = await userService.AddUser(user3);
                user4 = await userService.AddUser(user4);

                var group = new Group
                {
                    Name = "Group1"
                };

                group = await groupService.AddGroup(group);

                await groupService.AddUserToGroup(group.Id, user.Id);
                await groupService.AddUserToGroup(group.Id, user2.Id);
                await groupService.AddUserToGroup(group.Id, user3.Id);
                await groupService.AddUserToGroup(group.Id, user4.Id);
            }
        }

        [TestMethod]
        public async Task GeneratePairings_CreatesPairings()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var pairingService = new PairingService(context);
                List<Pairing> pairings = await pairingService.GenerateUserPairings(1);

                Assert.IsNotNull(pairings);
                Assert.IsTrue(pairings.Count == 4);
            }
        }

        [TestMethod]
        public async Task GeneratePairings_HasUniqueRecipient()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var pairingService = new PairingService(context);
                var userPairings = await pairingService.GenerateUserPairings(1);
                var sortedPairings = userPairings.OrderBy(pair => pair.RecipientId).ToList();
                
                for (var i = 0; i < sortedPairings.Count; i++)
                {
                    var pair = sortedPairings[i];
                    Assert.AreEqual(i + 1, pair.RecipientId);
                }
            }
        }

        [TestMethod]
        public async Task GeneratePairings_NoRecipientIsTheirOwnSanta()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var pairingService = new PairingService(context);
                List<Pairing> pairings = await pairingService.GenerateUserPairings(1);

                foreach (var pairing in pairings)
                {
                    Assert.AreNotEqual<int>(pairing.SantaId, pairing.RecipientId);
                }
            }
        }
    }
}
