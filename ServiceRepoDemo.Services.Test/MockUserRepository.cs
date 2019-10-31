using ServiceRepoDemo.Data;
using ServiceRepoDemo.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ServiceRepoDemo.Services.Test
{
    public class MockUserRepository : IRepository<User>
    {
        private List<User> Users;
        public MockUserRepository()
        {
            Users = new List<User> { 
                new User
                {
                    DateCreated = DateTime.UtcNow,
                    Email = "jumaniyozov@oybek.com",
                    FirstName = "Oybek",
                    LastName = "Jumaniyozov",
                    PasswordHash = "123456aa",
                    UserId = 1
                }
            };
        }
        public void Add(User entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));
            var largest = Users.Max(x => x.UserId) + 1;
            entry.UserId = largest;
            Users.Add(entry);
        }

        public IQueryable<User> GetAll()
        {
            return Users.ToList().AsQueryable();
        }

        public int SaveChanges()
        {
            return 0;
        }

        public IQueryable<User> Where(Expression<Func<User, bool>> predicate)
        {
            return Users.Where(predicate.Compile()).ToList().AsQueryable();
        }
    }
}
