using Microsoft.EntityFrameworkCore;
using ServiceRepoDemo.Data.Errors;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ServiceRepoDemo.Data.SqlServer
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext Context;
        public Repository(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            Context = context;
            Context.Database.EnsureCreated();
        }
        public void Add(T entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));
            Context.Set<T>().Add(entry);
        }

        public void Delete(T entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));
            Context.Entry(entry).State = EntityState.Deleted;
        }

        public IQueryable<T> GetAll()
        {
            return Context.Set<T>();
        }

        public int SaveChanges()
        {
            try
            {
                return Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Database exception", ex);
            }
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }
    }
}
