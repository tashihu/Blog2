﻿using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IRepository<T>
    {
        void Create(T e);
        T Get(int id);
        IEnumerable<T> Get(Func<T, bool> where=null,Func<T,int> orderBy=null);
        void Update(T e);
        void Delete(int id);
    }
}
