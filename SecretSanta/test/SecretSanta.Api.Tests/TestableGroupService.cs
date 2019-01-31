using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Api.Tests
{
    public class TestableGroupService : IGroupService
    {

        public Group AddGroup_Return { get; set; }
        public Group AddGroup_Group { get; set; }

        public Group AddGroup(Group group)
        {
            AddGroup_Group = group;
            return AddGroup_Return;
        }
        
        public Group DeleteGroup_Group { get; set; } 

        public void DeleteGroup(Group group)
        {
            DeleteGroup_Group = group;
            
        }

        public List<Group> FetchAll_Return { get; set; }
        public List<Group> FetchAll()
        {
            return FetchAll_Return;
        }

        public Group UpdateGroup_Return { get; set; }
        public Group UpdateGroup_Group { get; set; }
        public Group UpdateGroup(Group group)
        {
            UpdateGroup_Group = group;
            return UpdateGroup_Return;
        }

        public List<User> FetchAllUsersInGroup_Return { get; set; }
        public int FetchAllUsersInGroup_groupId { get; set; }
        public List<User> FetchAllUsersInGroup(int groupId)
        {
            FetchAllUsersInGroup_groupId = groupId;
            return FetchAllUsersInGroup_Return;
        }

        public int AddUserToGroup_groupId { get; set; }
        public User AddUserToGroup_User { get; set; }
        public User AddUserToGroup_Return { get; set; }
        public User AddUserToGroup(int groupId, User user)
        {
            AddUserToGroup_groupId = groupId;
            AddUserToGroup_User = user;
            return AddUserToGroup_Return;
        }
        public int RemoveUserFromGroup_groupId { get; set; }
        public User RemoveUserFromGroup_User { get; set; }
        public User RemoveUserFromGroup_Return { get; set; }
        public User RemoveUserFromGroup(int groupId, User user)
        {
            RemoveUserFromGroup_groupId = groupId;
            RemoveUserFromGroup_User = user;
            return RemoveUserFromGroup_Return;
        }
    }
}
