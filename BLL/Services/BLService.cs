using BLL.Interface;
using DAL.Interfaces;
using ORM;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BLL.Services
{
    
    public class BLService<T> : IService<T>
    {
        private readonly IRepository<T> repository;

        public BLService(IRepository<T> repository)
        {        
            this.repository = repository;
        }
        public void Create(T e)
        {
            repository.Create(e);
        }
        public T Get(int id)
        {
            return repository.Get(id);
        }
        public IEnumerable<T> Get(Func<T, bool> predicate = null,Func<T,int> orderBy=null)
        {
            return repository.Get(where: predicate, orderBy: orderBy);
        }
        public void Update(T e)
        {
            repository.Update(e);
        }
        public void Delete(int id)
        {
            repository.Delete(id);
        }        
    }
}
