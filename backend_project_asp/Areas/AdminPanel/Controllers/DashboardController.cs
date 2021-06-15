using backend_project_asp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_project_asp.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles =RoleConstants.Admin)]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}
