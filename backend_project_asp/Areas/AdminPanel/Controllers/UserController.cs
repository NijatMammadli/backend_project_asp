using backend_project_asp.Data;
using backend_project_asp.Models;
using backend_project_asp.ViewModels;
using FrontToBack_hw.DataAccessLayer;
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
    [Authorize(Roles = RoleConstants.Admin)]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _dbContext;

        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, AppDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
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
                    Role = (await _userManager.GetRolesAsync(dbuser)).FirstOrDefault(),
                    IsDeactive = dbuser.IsDeactive

                };

                users.Add(user);
            }

            return View(users);

        }
        public async Task<IActionResult> ChangeRole(string id)
        {
            if (id == null) return NotFound();

            User user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            List<string> roles = new List<string>();
            roles.Add(RoleConstants.Admin);
            roles.Add(RoleConstants.CourseModerator);
            roles.Add(RoleConstants.UserRole);
            ChangeRoleViewModel changeRole = new ChangeRoleViewModel
            {
                Roles = roles,
                Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault(),
                Courses = await _dbContext.Courses.Where(x => x.UserId == id).ToListAsync(),
            };

            var courses = _dbContext.Courses.Where(x => x.IsDeleted == false).ToList();
            ViewBag.Courses = courses;

            return View(changeRole);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeRole(string id, ChangeRoleViewModel changeRole)
        {

            if (id == null) return NotFound();

            User user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            List<string> roles = new List<string>();
            roles.Add(RoleConstants.Admin);
            roles.Add(RoleConstants.CourseModerator);
            roles.Add(RoleConstants.UserRole);
            ChangeRoleViewModel dbChangeRole = new ChangeRoleViewModel
            {
                Roles = roles,
                Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault(),
                Courses = await _dbContext.Courses.Where(x => x.UserId == id).ToListAsync(),
            };

            var courses = _dbContext.Courses.Where(x => x.IsDeleted == false).ToList();
            ViewBag.Courses = courses;
            if (dbChangeRole.Role != changeRole.Role)
            {
                IdentityResult addResult = await _userManager.AddToRoleAsync(user, changeRole.Role);
                if (!addResult.Succeeded)
                {
                    ModelState.AddModelError("", "Some problem exist");

                    return View(dbChangeRole);
                }

                IdentityResult removeResult = await _userManager.RemoveFromRoleAsync(user, dbChangeRole.Role);
                if (!removeResult.Succeeded)
                {
                    ModelState.AddModelError("", "Some problem exist");
                    return View(dbChangeRole);
                }
                if (dbChangeRole.Role == RoleConstants.CourseModerator)
                {
                    foreach (Course course in dbChangeRole.Courses)
                    {
                        course.UserId = null;
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }
            if (changeRole.Role == RoleConstants.CourseModerator)
            {
                foreach (Course course in dbChangeRole.Courses)
                {
                    course.UserId = null;
                }
                foreach (int courseId in changeRole.CoursesId)
                {

                    var course = await _dbContext.Courses.FirstOrDefaultAsync(x => x.Id == courseId);
                    course.UserId = id;
                }


                await _dbContext.SaveChangesAsync();

            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Activated(string id)
        {
            if (id == null) return NotFound();
            User user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            if (user.IsDeactive)
            {
                user.IsDeactive = false;

            }
            else
            {
                user.IsDeactive = true;
                 var courses = await _dbContext.Courses.Where(x => x.UserId == id).ToListAsync();
                foreach (Course course in courses)
                {
                    course.UserId = null;

                }
                await _dbContext.SaveChangesAsync(); 
            }
            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index");
        }

    }
}
