using backend_project_asp.Models;
using backend_project_asp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_project_asp.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

      

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> SignUp(RegisterViewModel register)
        {
            if(!ModelState.IsValid)
            {
                return View(); 
            }

            var dbUser = await _userManager.FindByNameAsync(register.Username);

            if (dbUser != null)
            {
                ModelState.AddModelError("Username", "Username is already used!");
                return View();
            }



            var newUser = new User
            {
                UserName = register.Username,
                Fullname = register.Fullname,
                Email = register.Email
            };

           var identityResult = await _userManager.CreateAsync(newUser, register.Password);

            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();


            }


            await _signInManager.SignInAsync(newUser, true);

            return RedirectToAction("Index", "Home"); 


        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
