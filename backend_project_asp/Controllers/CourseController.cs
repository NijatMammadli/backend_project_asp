
using backend_project_asp.Models;
using backend_project_asp.ViewModels;
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

        public  IActionResult Index(int? categoryId,int page=1)
        {
            if (categoryId == null)
            {
                ViewBag.PageCount = Decimal.Ceiling(_dbcontext.Courses.Where(x => x.IsDeleted == false).Count() / 3);
                ViewBag.Page = page;
                return View();

            }
            else
            {
                List<Course> courses = new List<Course>();
                IQueryable<CourseCategory> courseCategories = _dbcontext.CourseCategories.Where(x => x.CategoryId == categoryId).Include(x => x.Course);
                foreach (CourseCategory ct in courseCategories)
                {
                    courses.Add(ct.Course);
                }
                return View(courses);

            }
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

            var categories = await _dbcontext.Categories.Include(x => x.CourseCategories).ToListAsync();

            var courseViewModel = new CourseViewModel
            {
                CourseDetail = courseDetail,
                Categories = categories
            };

            return View(courseViewModel); 
        }

    }
}
