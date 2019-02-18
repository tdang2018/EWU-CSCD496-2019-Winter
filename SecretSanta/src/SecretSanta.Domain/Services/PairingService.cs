using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Domain.Services
{
    public class PairingService : IPairingService
    {
        public ApplicationDbContext DbContext { get; set; }

        private IRandomService RandomService { get; }

        public PairingService(ApplicationDbContext dbContext, IRandomService randomService=null)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            RandomService = randomService ?? new RandomService();
        }

        public async Task<bool> GeneratePairingsForGroup(int groupId)
        {
            Group group = await DbContext.Groups
                .Include(g => g.GroupUsers)
                .FirstOrDefaultAsync(g => g.Id == groupId);

            List<int> userIds = group?.GroupUsers?.Select(x => x.UserId).ToList();

            if (userIds is null || userIds.Count < 2) return false;

            Task<List<Pairing>> task = Task.Run(() => GenerateUserPairings(userIds));
            List<Pairing> pairings = await task;

            await DbContext.Pairings.AddRangeAsync(pairings);
            await DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<Pairing>> GenerateUserPairings(int groupId)
        {
            var userIds = await DbContext.Groups
                .Where(g => g.Id == groupId)
                .SelectMany(g => g.GroupUsers, (g, gu) => gu.UserId)
                .ToListAsync();

            List<Pairing> pairings = await Task.Run(() => GenerateUserPairings(userIds));

            await DbContext.Pairings.AddRangeAsync(pairings);
            await DbContext.SaveChangesAsync();
            return pairings;
        }

        private  List<Pairing> GenerateUserPairings(List<int> userIds)
        {     
            var pairings = new List<Pairing>();
            var random = RandomService;
            var randUserIds = userIds.OrderBy(id => random.Next()).ToList();

            for (var i = 0; i < randUserIds.Count - 1; i++)
            {
                var paring = new Pairing
                {
                    RecipientId = randUserIds[i],
                    SantaId = randUserIds[i + 1]
                };                
                pairings.Add(paring);
            }
            var lastPairing = new Pairing
            {
                SantaId = randUserIds.First(),
                RecipientId = randUserIds.Last()
            };           
            pairings.Add(lastPairing);
            return pairings;
        }
    }
}
