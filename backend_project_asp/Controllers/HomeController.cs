using backend_project_asp.Models;
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

            var isExist = await _dbContext.Subscribes.AnyAsync(x=> x.Email==email);
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

        
    }
}
