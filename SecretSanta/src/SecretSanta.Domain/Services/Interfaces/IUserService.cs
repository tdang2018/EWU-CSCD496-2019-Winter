using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Services.Interfaces
{
    public interface IUserService
    {
        List<User> FetchAll();
        User GetById(int id);
        User AddUser(User user);
        User UpdateUser(User user);
        bool DeleteUser(int userId);
    }
}
