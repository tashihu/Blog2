using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IRepository<T>
    {
        void Create(T e);
        T Get(int id);
        IEnumerable<T> GetAll();
        void Update(T e);
        void Delete(int id);
    }
}
