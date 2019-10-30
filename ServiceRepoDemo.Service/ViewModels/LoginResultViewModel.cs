using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceRepoDemo.Service.ViewModels
{
    public class LoginResultViewModel
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
