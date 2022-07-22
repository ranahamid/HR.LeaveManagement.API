using System.Threading.Tasks;
using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Models;
using HR.LeaveManagement.MVC.Services; 
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IAuthenticationService _authService;

        public UsersController(IAuthenticationService authService)
        {
            _authService = authService;
        }
        public IActionResult Login(string returnUrl= null)
        {
            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> Login(LoginVm login, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                returnUrl ??= Url.Content("~/");
                var isLogSuccess = await _authService.Authenticate(login.Email, login.Password);
                if (isLogSuccess)
                {
                    return LocalRedirect(returnUrl);
                } 
            }
            ModelState.AddModelError("", "Log In Attempt Failed. Please try again.");
            return View(login);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm register, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                returnUrl ??= Url.Content("~/");
                var isRegisterSuccess = await _authService.Register(register);
                if (isRegisterSuccess)
                {
                    return LocalRedirect(returnUrl);
                }
            }
            ModelState.AddModelError("", "Registration Attempt Failed. Please try again.");
            return View(register);
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            returnUrl ??= Url.Content("~/");
            await _authService.Logout();
            return LocalRedirect(returnUrl);
        }
    }
}
