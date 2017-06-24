using DAL.Interfaces;
using ORM;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext context;

        public UserRepository(DbContext uow)
        {
            this.context = uow;
        }
        public void Create(Users e)
        {           
            context.Set<Users>().Add(e);
            context.SaveChanges();
        }
        public Users Get(int id)
        {
            return context.Set<Users>()
                        .Where(user => user.UserId.Equals(id))
                        .Select(user => user)
                        .FirstOrDefault();
        }

        public Users GetUserByEmail(string email)
        {
            return context.Set<Users>()
                        .Where(user => user.Email.Equals(email))
                        .Select(user => user)
                        .FirstOrDefault();
        }

        public IEnumerable<Users> GetAll()
        {
            return context.Set<Users>().Select(user => user);
        }
        public void Update(Users e)
        {
            
            var users = context.Set<ORM.Users>()
                            .Where(user => user.UserId.Equals(e.UserId))
                            .Select(user => user)
                            .FirstOrDefault();
            users = e;            
            
            context.SaveChanges();
            context.Dispose();
        }
        public void Delete(int id)
        {
            var users = context.Set<ORM.Users>()
                        .Where(user => user.UserId.Equals(id))
                        .FirstOrDefault();

            context.Set<ORM.Users>().Remove(users);
            context.SaveChanges();
            context.Dispose();
        }

        public Users GetUserByName(string name)
        {
            return context.Set<Users>()
                           .Where(user => user.Name.Equals(name))
                           .Select(user => user)
                           .FirstOrDefault();
        }
    }
}
