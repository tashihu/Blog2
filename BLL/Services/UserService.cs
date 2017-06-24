using BLL.Interface;
using DAL.Interfaces;
using ORM;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BLL.Services
{
    public class UserService : IUSerService
    {        
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository repository)
        {        
            this.userRepository = repository;
        }
        public void Create(Users e)
        {
            userRepository.Create(e);
        }
        public Users Get(int id)
        {
            return userRepository.Get(id);
        }
        public IEnumerable<Users> GetAll()
        {
            return userRepository.GetAll().Select(user => user);
        }
        public void Update(Users e)
        {
            userRepository.Update(e);
        }
        public void Delete(int id)
        {
            userRepository.Delete(id);
        }

        public Users getUserByEmail(string email)
        {
            return userRepository.GetUserByEmail(email);
        }

        public Users getUserByName(string name)
        {
            return userRepository.GetUserByEmail(name);
        }
    }
}
