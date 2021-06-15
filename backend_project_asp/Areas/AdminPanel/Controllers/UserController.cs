using backend_project_asp.Data;
using backend_project_asp.Models;
using backend_project_asp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_project_asp.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles =RoleConstants.Admin)]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task< IActionResult> Index()
        {
            var dbusers = await _userManager.Users.ToListAsync();


            var users = new List<UserViewModel>();

            foreach (var dbuser in dbusers)
            {
                var user = new UserViewModel
                {
                    Id = dbuser.Id,
                    Username = dbuser.UserName,
                    Fullname = dbuser.Fullname,
                    Email = dbuser.Email,
                    Role = (await _userManager.GetRolesAsync(dbuser)).FirstOrDefault()
                };

                users.Add(user);
            }

            return View(users);

        }
    }
}
