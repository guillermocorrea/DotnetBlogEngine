using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersRepository _usersRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            IActionResult HandleError()
            {
                ViewData["Error"] = true;
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return HandleError();
            }

            var user = await _usersRepository.GetByUsernameAndPassword(model.Username, model.Password);
            if (user == null)
            {
                return HandleError();
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim("username", user.Username),
                new Claim("id", user.Id.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
            var userPrincipal = new ClaimsPrincipal(new[] { claimsIdentity });
            await HttpContext.SignInAsync(userPrincipal);
            
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}