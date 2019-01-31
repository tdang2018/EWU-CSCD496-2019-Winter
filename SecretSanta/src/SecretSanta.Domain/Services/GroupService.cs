using System;
using System.Collections.Generic;
using System.Linq;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public class GroupService
    {
        private ApplicationDbContext DbContext { get; }

        public GroupService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Group AddGroup(Group Group)
        {
            DbContext.Groups.Add(Group);
            DbContext.SaveChanges();
            return Group;
        }

        public Group UpdateGroup(Group Group)
        {
            DbContext.Groups.Update(Group);
            DbContext.SaveChanges();
            return Group;
        }

        public List<Group> FetchAll()
        {
            return DbContext.Groups.ToList();
        }

        public List<User> GetUsers(int groupId)
        {
            return DbContext.Groups
                .Where(g => g.Id == groupId)
                .SelectMany(g => g.GroupUsers)
                .Select(gu => gu.User)
                .ToList();

        }
    }
}