using backend_project_asp.Models;
using backend_project_asp.ViewModels;
using FrontToBack_hw.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace backend_project_asp.Controllers
{
    public class HomeController : Controller
    {

        private readonly AppDbContext _dbContext;

        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var homeSliders = _dbContext.HomeSliders.ToList();

            return View(homeSliders);
        }

        public async Task<IActionResult> SubscribeAsync(string email)
        {
            if (email == null)
            {
                return Content("Email cannot be null");
            }
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (!match.Success)
            {
                return Content("Please provide correct email address");
            }

            var isExist = await _dbContext.Subscribes.AnyAsync(x => x.Email == email);
            if (isExist)
            {
                return Content("You have already subscribed");
            }

            Subscribe subscribe = new Subscribe()
            {
                Email = email
            };
            await _dbContext.Subscribes.AddAsync(subscribe);
            await _dbContext.SaveChangesAsync();
            return Content("You have been successfully subscribed");
        }

        public async Task<IActionResult> GlobalSearch(string search)
        {
            if (search == null) return NotFound();
            GlobalSearchViewModel globalSearch = new GlobalSearchViewModel
            {
                Courses = await _dbContext.Courses.Where(p => p.Name.Contains(search) && p.IsDeleted == false).OrderByDescending(p => p.Id).Take(3).ToListAsync(),
                Teachers = await _dbContext.Teachers.Where(p => p.Name.Contains(search) && p.IsDeleted == false).OrderByDescending(p => p.Id).Take(3).ToListAsync(),
                Blogs = await _dbContext.Blogs.Where(p => p.Name.Contains(search) && p.IsDeleted == false).OrderByDescending(p => p.Id).Take(3).ToListAsync(),
                Events = await _dbContext.Events.Where(p => p.Name.Contains(search) && p.IsDeleted == false).OrderByDescending(p => p.Id).Take(3).ToListAsync(),
            };

            return PartialView("_GlobalSearchPartial", globalSearch);
        }

    }
}
