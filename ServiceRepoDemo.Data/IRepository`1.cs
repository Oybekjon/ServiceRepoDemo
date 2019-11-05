using System;
using System.Linq;
using System.Linq.Expressions;

namespace ServiceRepoDemo.Data
{
    public interface IRepository<T> where T : class
    {
        void Add(T entry);
        int SaveChanges();
        void Delete(T entry);
        IQueryable<T> GetAll();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
    }
}
