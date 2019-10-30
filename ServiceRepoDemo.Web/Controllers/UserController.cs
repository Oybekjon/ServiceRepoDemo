using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceRepoDemo.Data.Errors;
using ServiceRepoDemo.Service;
using ServiceRepoDemo.Service.Errors;
using ServiceRepoDemo.Service.ViewModels;

namespace ServiceRepoDemo.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService UserService;
        public UserController(ILogger<HomeController> logger, IUserService service)
        {
            UserService = service;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login([FromBody] LoginViewModel viewModel)
        {
            return Execute(() =>
            {
                var result = UserService.Login(viewModel);
                return Json(result);
            });
        }

        protected IActionResult Execute(Func<IActionResult> func)
        {
            try
            {
                return func();
            }
            catch (WeakPasswordException ex)
            {
                _logger.LogDebug("Weak password", ex);
                return Json(new { Message = "Password is weak" });
            }
            catch (DuplicateEmailException ex)
            {
                _logger.LogDebug("Eamil dup", ex);
                return Json(new { Message = "Duplicate email" });
            }
            catch (PasswordMismatchException ex)
            {
                _logger.LogDebug("Mismatch", ex);
                return Json(new { Message = "Passwords dont match" });
            }
            // SqlServer 
            // MongoDbException
            catch (DatabaseException ex)
            {
                _logger.LogError("Database exception", ex);
                return DatabaseError();
            }
            catch (Exception ex)
            {
                _logger.LogError("Mismatch", ex);
                return ServerError();
            }
        }

        public IActionResult DatabaseError()
        {
            Response.StatusCode = 500;
            return Json(new { Message = "Database error" });
        }

        public IActionResult ServerError()
        {
            Response.StatusCode = 500;
            return Json(new { Message = "Server error" });
        }
    }
}