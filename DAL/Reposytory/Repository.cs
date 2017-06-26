using DAL.Interfaces;
using ORM;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace DAL
{
    public class Repository<T> : IRepository<T> where T:class
    {
        private readonly DbContext context;

        public Repository(DbContext uow)
        {
            this.context = uow;
        }
        public void Create(T e)
        {
            context.Set<T>().Add(e);
            context.SaveChanges();
        }
        public T Get(int id)
        {
            return context.Set<T>().Find(id);
        }
        public IEnumerable<T> Get(Func<T, bool> predicate)
        {
            return context.Set<T>().AsNoTracking().Where(predicate).ToList();
        }
        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().Select(user => user).ToList();
        }
        public void Update(T e)
        {
            context.Entry(e).State = EntityState.Modified;
            context.SaveChanges();
        }
        public void Delete(int id)
        {            
            context.Set<T>().Remove(Get(id));
            context.SaveChanges();         
        }
    }
}

