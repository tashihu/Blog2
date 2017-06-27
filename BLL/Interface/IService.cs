using System;
using System.Collections.Generic;

namespace BLL.Interface
{
    public interface IService<T>    
    {        
        T Get(int id);
        IEnumerable<T> Get(Func<T, bool> where = null,Func<T,int> orderBy = null);
        void Create(T e);
        void Update(T e);
        void Delete(int id);
    }
}
