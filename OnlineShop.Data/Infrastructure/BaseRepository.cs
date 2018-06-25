using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace OnlineShop.Data.Infrastructure
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        #region Properties

        private OnlineShopDbContext dbContext;
        private readonly IDbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected OnlineShopDbContext DbContext
        {
            get
            {
                return dbContext ?? (dbContext = DbFactory.Init());
            }
        }

        #endregion Properties

        #region Constructor

        protected BaseRepository(IDbFactory dbFactory)
        {
            this.DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }

        #endregion Constructor

        #region Methods Implementation

        public virtual T Add(T entity)
        {
            return dbSet.Add(entity);
        }

        public virtual bool CheckContains(Expression<Func<T, bool>> predicate)
        {
            return dbContext.Set<T>().Count<T>(predicate) > 0;
        }

        public virtual int Count(Expression<Func<T, bool>> where)
        {
            return dbSet.Count(where);
        }

        public virtual T Delete(T entity)
        {
            return dbSet.Remove(entity);
        }

        public virtual T Delete(int id)
        {
            var entity = dbSet.Find(id);
            return dbSet.Remove(entity);
        }

        public virtual void DeleteMulti(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T item in objects)
                dbSet.Remove(item);
        }

        public virtual IEnumerable<T> GetAll(string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = dbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.AsQueryable();
            }
            return dbContext.Set<T>().AsQueryable();
        }

        public virtual IEnumerable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = dbContext.Set<T>().Include(includes.First());
                foreach (var include in includes)
                    query = query.Include(include);
                return query.Where<T>(predicate).AsQueryable<T>();
            }
            return dbContext.Set<T>().Where<T>(predicate).AsQueryable<T>();
        }

        public virtual IEnumerable<T> GetMultiPaging(Expression<Func<T, bool>> predicate, out int total, int index = 0, int size = 50, string[] includes = null)
        {
            int skipCount = index * size;
            IQueryable<T> _resetSet;

            if (includes != null && includes.Count() > 0)
            {
                var query = dbContext.Set<T>().Include(includes.First());
                foreach (var include in includes)
                    query = query.Include(include);
                _resetSet = predicate != null ? query.Where<T>(predicate).AsQueryable() : query.AsQueryable();
            }
            else
            {
                _resetSet = predicate != null ? dbContext.Set<T>().Where<T>(predicate).AsQueryable() : dbContext.Set<T>().AsQueryable();
            }

            _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }

        public virtual T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = dbContext.Set<T>().Include(includes.First());
                foreach (var include in includes)
                    query = query.Include(include);
                return query.FirstOrDefault(expression);
            }
            return dbContext.Set<T>().FirstOrDefault(expression);
        }

        public virtual T GetSingleByID(int id)
        {
            return dbSet.Find(id);
        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
        }

        #endregion Methods Implementation
    }
}