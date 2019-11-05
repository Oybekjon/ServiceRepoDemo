using System;
using System.Collections.Generic;

namespace ServiceRepoDemo.DomainObjects
{
    public class User
    {
        private ICollection<Item> _Items;
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime DateCreated { get; set; }

        public ICollection<Item> Items
        {
            get => _Items = _Items ?? new List<Item>();
            set => _Items = value;
        }
    }
}
