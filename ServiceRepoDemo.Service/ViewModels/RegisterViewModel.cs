﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceRepoDemo.Service.ViewModels
{
    public class RegisterViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public bool Agree { get; set; }
    }
}
