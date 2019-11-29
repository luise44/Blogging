using BlogEngine.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BlogEngine.Domain.Models;
using BlogEngine.Domain.Services.Services;

namespace BlogEngine.Domain.Services.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly BlogEngineContext _context;

        public UserRepository(BlogEngineContext context)
        {
            _context = context;
        }

        public IList<User> GetAll()
        {
            return _context.Users.ToArray();
        }

        public User GetById(int id)
        {
            return _context.Users.Where(x=> x.Id == id).FirstOrDefault();
        }

        public User GetByCredentials(string name, string password)
        {
            return _context.Users.Where(x=> x.Name == name && x.Password == password).FirstOrDefault();
        }
    }
}
