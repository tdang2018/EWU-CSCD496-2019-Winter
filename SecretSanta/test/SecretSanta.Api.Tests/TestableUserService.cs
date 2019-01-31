using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Api.Tests
{
    public class TestableUserService : IUserService
    {
        public User AddUser_Return { get; set; }
        public User AddUser_User { get; set; }
        public User AddUser(User user)
        {
            AddUser_User = user;
            return AddUser_Return;
        }

        public User DeleteUser_User { get; set; }
        public void DeleteUser(User user)
        {
            DeleteUser_User = user;
        }

        public User UpdateUser_User { get; set; }
        public User UpdateUser_Return{get; set;}
        public User UpdateUser(User user)
        {
            UpdateUser_User = user;
            return UpdateUser_Return;
        }
    }
}
