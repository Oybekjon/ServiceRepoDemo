using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceRepoDemo.Data;
using ServiceRepoDemo.DomainObjects;
using ServiceRepoDemo.Service.Errors;
using ServiceRepoDemo.Service.Implementation;
using ServiceRepoDemo.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ServiceRepoDemo.Services.Test
{
    [TestClass]
    public class UserServiceTest
    {

        private Mock<IRepository<User>> UserRepositoryMock;

        [TestMethod]
        public void TestLoginSuccess()
        {
            SetupRepository();
            var service = new UserService(UserRepositoryMock.Object);

            var viewModel = new LoginViewModel
            {
                Email = "jumaniyozov@oybek.com",
                Password = "123456aa"
            };
            var result = service.Login(viewModel);
            Assert.IsNotNull(result);
        }



        [TestMethod]
        public void TestRegister_Duplicate()
        {
            var repo = new MockUserRepository();
            var service = new UserService(repo);

            var viewModel = new RegisterViewModel
            {
                Agree = true,
                Email = "jumaniyozov@oybek.com",
                FirstName = "Oybek",
                LastName = "Jumaniyozov",
                Password = "123456aa",
                PasswordConfirmation = "123456aa"
            };

            Assert.ThrowsException<DuplicateEmailException>(() =>
            {
                service.Register(viewModel);
            });
        }

        [TestMethod]
        public void TestChangePassword_Success()
        {
            var repo = new MockUserRepository();
            var service = new UserService(repo);

            var viewModel = new ChangePasswordViewModel
            {
                CurrentPassword = "123456aa",
                NewPassword = "1qalaksjflkasj",
                NewPasswordConfirmation = "1qalaksjflkasj",
                UserId = 1
            };

            var result = service.ChangePassword(viewModel);
        }

        [TestMethod]
        public void TestChangePassword_IncorrectOldPassword()
        {
            SetupRepository();
            var service = new UserService(UserRepositoryMock.Object);

            var viewModel = new ChangePasswordViewModel
            {
                CurrentPassword = "Incorrect Old password",
                NewPassword = "1qalaksjflkasj",
                NewPasswordConfirmation = "1qalaksjflkasj",
                UserId = 1
            };

            Assert.ThrowsException<OldPasswordIncorrectException>(() =>
            {
                service.ChangePassword(viewModel);
            });
        }

        [TestMethod]
        public void TestChangePassword_WeakPasswordFail()
        {
            var repo = new MockUserRepository();
            var service = new UserService(repo);

            var viewModel = new ChangePasswordViewModel
            {
                CurrentPassword = "123456aa",
                NewPassword = "123",
                NewPasswordConfirmation = "123",
                UserId = 1
            };

            Assert.ThrowsException<WeakPasswordException>(() =>
            {
                service.ChangePassword(viewModel);
            });
        }

        [TestMethod]
        public void TestChangePassword_PasswordMismatchFail()
        {
            var repo = new MockUserRepository();
            var service = new UserService(repo);

            var viewModel = new ChangePasswordViewModel
            {
                CurrentPassword = "123456aa",
                NewPassword = "asdfasdfasd987897",
                NewPasswordConfirmation = "Mismatching password",
                UserId = 1
            };

            Assert.ThrowsException<PasswordMismatchException>(() =>
            {
                service.ChangePassword(viewModel);
            });
        }

        private void SetupRepository()
        {
            UserRepositoryMock = new Mock<IRepository<User>>();
            UserRepositoryMock.Setup(x => x.Where(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new List<User> { 
                    new User
                    {
                        DateCreated = DateTime.UtcNow,
                        Email = "jumaniyozov@oybek.com",
                        FirstName = "From moq",
                        LastName = "Jumaniyozov",
                        PasswordHash = "123456aa",
                        UserId = 1
                    }
                }.AsQueryable());
        }
    }
}
