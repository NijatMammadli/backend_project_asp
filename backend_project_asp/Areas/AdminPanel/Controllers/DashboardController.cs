using backend_project_asp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_project_asp.Areas.AdminPanel;
using backend_project_asp.Areas.AdminPanel.Utils;
using backend_project_asp.Models;
using FrontToBack_hw.DataAccessLayer;

namespace backend_project_asp.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles =RoleConstants.Admin)]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _dbContext;

        public DashboardController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
            
        }


       
    }
}
