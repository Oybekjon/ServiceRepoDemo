using ServiceRepoDemo.Service.ViewModels;
using System;

namespace ServiceRepoDemo.Service
{
    public interface IUserService
    {
        LoginResultViewModel Login(LoginViewModel viewModel);
        RegisterResultViewModel Register(RegisterViewModel viewModel);
        /// <summary>
        /// Throws WeakPasswordException if password is not correct
        /// </summary>
        /// <param name="password"></param>
        void ValidatePassword(string password);
    }
}
