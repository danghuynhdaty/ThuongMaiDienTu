﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OnlineShop.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        //Marks an entity as new
        T Add(T entity);

        // Marks an entity as modified
        void Update(T entity);

        //Marks an entity to be removed
        T Delete(T entity);

        T Delete(int id);

        //Delete multi records
        void DeleteMulti(Expression<Func<T, bool>> where);

        //Get an enity by int id
        T GetSingleByID(int id);

        //Get an entity by condition
        T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null);

        //Get all entity
        IEnumerable<T> GetAll(string[] includes = null);

        IEnumerable<T> GetMultiPaging(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50, string[] includes = null);

        IEnumerable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null);

        int Count(Expression<Func<T, bool>> where);

        bool CheckContains(Expression<Func<T, bool>> predicate);
    }
}