using BLL.Interface;
using DAL.Interfaces;
using ORM;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class RoleService : IService<Roles>
    {
        private readonly IRepository<Roles> RoleRepository;

        public RoleService(IRepository<Roles> repository)
        {
            this.RoleRepository = repository;
        }

        public void Create(Roles e)
        {
            RoleRepository.Create(e);
        }
        public Roles Get(int id)
        {
            return RoleRepository.Get(id);
        }
        public IEnumerable<Roles> GetAll()
        {
            return RoleRepository.GetAll().Select(role => role);
        }
        public void Update(Roles e)
        {
            RoleRepository.Update(e);
        }
        public void Delete(int id)
        {
            RoleRepository.Delete(id);
        }
    }
}
