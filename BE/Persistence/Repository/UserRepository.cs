using Domain.Entities;
using Domain.IRepositories;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public  class UserRepository : IUserRepository
    {
        public MyDbContext _myDbContext;
        public UserRepository(MyDbContext myDbContext) {
            _myDbContext = myDbContext;
        }
        public User GetEntityUser(string username, string password)
        {
           return _myDbContext.Users.FirstOrDefault(x=> x.Username.Equals(username) && x.Password.Equals(password));
        }
        public User GetEntityUser(string username)
        {
            return _myDbContext.Users.FirstOrDefault(x => x.Username.Equals(username));
        }
    }
}
