using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceRepoDemo.Data;
using ServiceRepoDemo.DomainObjects;
using ServiceRepoDemo.Service.Errors;
using ServiceRepoDemo.Service.ViewModels;

namespace ServiceRepoDemo.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> Repository;
        public UserService(IRepository<User> repository)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public bool ChangePassword(ChangePasswordViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public LoginResultViewModel Login(LoginViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            if (string.IsNullOrWhiteSpace(viewModel.Email))
                throw new ArgumentException("Email cannot be empty");

            if (string.IsNullOrWhiteSpace(viewModel.Password))
                throw new ArgumentException("Password is empty");

            var hash = HashPassword(viewModel.Password);

            var user = Repository.Where(x => x.Email == viewModel.Email && x.PasswordHash == hash).FirstOrDefault();
            if (user == null)
                throw new AuthenticationException();
            return new LoginResultViewModel
            {
                DateCreated = user.DateCreated,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserId = user.UserId
            };
        }

        public RegisterResultViewModel Register(RegisterViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            if (string.IsNullOrWhiteSpace(viewModel.Email))
                throw new ArgumentException("Email cannot be empty");

            if (string.IsNullOrWhiteSpace(viewModel.Password))
                throw new ArgumentException("Password is empty");

            ValidatePassword(viewModel.Password);
            var hash = HashPassword(viewModel.Password);

            var exists = Repository.Where(x => x.Email == viewModel.Email).Any();
            if (exists)
                throw new DuplicateEmailException();

            var user = new User();
            user.DateCreated = DateTime.UtcNow;
            user.Email = viewModel.Email;
            user.FirstName = viewModel.FirstName;
            user.LastName = viewModel.LastName;
            user.PasswordHash = hash;
            Repository.Add(user);
            Repository.SaveChanges();
            return new RegisterResultViewModel
            {
                Message = "User has been created",
                Success = true
            };
        }

        public void ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                throw new WeakPasswordException();
        }

        private string HashPassword(string password)
        {
            return password;
        }
    }
}
