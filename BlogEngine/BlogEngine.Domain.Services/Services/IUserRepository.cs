using BlogEngine.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Domain.Services.Services
{
    public interface IUserRepository
    {
        IList<User> GetAll();
        User GetById(int id);
        User GetByCredentials(string name, string password);
    }
}
