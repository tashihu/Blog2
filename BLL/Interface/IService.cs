﻿using System.Collections.Generic;

namespace BLL.Interface
{
    public interface IService<T> 
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        void Create(T e);
        void Update(T e);
        void Delete(int id);
    }
}
