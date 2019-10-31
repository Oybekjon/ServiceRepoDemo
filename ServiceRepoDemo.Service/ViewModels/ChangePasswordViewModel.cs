using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceRepoDemo.Service.ViewModels
{
    public class ChangePasswordViewModel
    {
        public long UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordConfirmation { get; set; }
    }
}
