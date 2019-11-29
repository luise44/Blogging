using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlogEngine.Domain.Services.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using static BlogEngine.Core.Enums;

namespace BlogEngine.Controllers
{
    public class AccountController : Controller
    {
        IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = _userRepository.GetByCredentials(email, password);            
            if (user == null)
            {
                ModelState.AddModelError("", "User not found");
                return View();
            }

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
            identity.AddClaim(new Claim("Id", user.Id.ToString()));
            switch (user.Type)
            {
                case 1:
                    identity.AddClaim(new Claim("Role", UserRoleEnum.Editor.ToString()));
                    identity.AddClaim(new Claim(ClaimTypes.Role, UserRoleEnum.Editor.ToString()));
                    break;
                case 2:
                    identity.AddClaim(new Claim("Role", UserRoleEnum.Writer.ToString()));
                    identity.AddClaim(new Claim(ClaimTypes.Role, UserRoleEnum.Writer.ToString()));
                    break;
            }
            
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}