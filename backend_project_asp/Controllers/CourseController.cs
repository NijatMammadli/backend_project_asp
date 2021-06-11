
using backend_project_asp.Models;
using FrontToBack_hw.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_project_asp.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _dbcontext;

        public CourseController(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public  IActionResult Index(int page=1)
        {
            ViewBag.PageCount = Decimal.Ceiling(_dbcontext.Courses.Where(x=>x.IsDeleted==false).Count()/3);
            ViewBag.Page = page; 
            
            

            return View();
        }


        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound(); 
            }

            var courseDetail = await _dbcontext.CourseDetails.Include(x => x.Course).FirstOrDefaultAsync(x => x.CourseId == id);

            if (courseDetail == null)
            {
                return NotFound(); 
            }

            return View(courseDetail); 
        }

    }
}
