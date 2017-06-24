using DAL.Interfaces;
using ORM;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    public class RoleRepository:IRepository<Roles>
    {
        private readonly DbContext context;

        public RoleRepository(DbContext uow)
        {
            this.context = uow;
        }

        public void Create(Roles e)
        {            
            context.Set<Roles>().Add(e);            
        }
        public void Update(Roles e)
        {
            var roles = context.Set<ORM.Roles>()
                            .Where(role => role.RoleId.Equals(e.RoleId))
                            .Select(role => role)
                            .FirstOrDefault();
            roles.Name = e.Name;
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            var roles = context.Set<ORM.Roles>()
                            .Where(role => role.RoleId.Equals(id))
                            .FirstOrDefault();
            context.Set<ORM.Roles>().Remove(roles);
            context.SaveChanges();
        }
        public Roles Get(int id)
        {
            return context.Set<Roles>()
                                .Where(roles=> roles.RoleId.Equals(id))
                                .Select(roles => roles)
                                .FirstOrDefault();
        }
        public IEnumerable<Roles> GetAll()
        {
            return context.Set<Roles>().Select(user => user);
        }
    }
}
