﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    public  interface IRepository<T> where T : class
    {
        // T - Category
        IEnumerable<T> GetAll();   
        T Get(Expression<Func<T, bool>> filter);

        //Create , update or remove category

        void Add(T entity);

       /* void Update(T entity);*/        
        void Delete(T entity);
        void RemoveRange(IEnumerable<T> entity); // removing multiple category in single call



    }
}
