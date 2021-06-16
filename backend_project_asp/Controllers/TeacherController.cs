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
    public class TeacherController : Controller
    {
        private readonly AppDbContext _dbContext;

        public TeacherController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int? page=1)
        {
            ViewBag.PageCount = Decimal.Ceiling(_dbContext.Teachers.Where(x => x.IsDeleted == false).Count() / 4);
            ViewBag.Page = page;
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound(); 
            }

            var teacherDetail = await _dbContext.TeacherDetails.Include(x => x.Teacher).Where(x => x.IsDeleted == false)
                .Include(x => x.Teacher.socialMedias).Include(x => x.Teacher.Position).FirstOrDefaultAsync(x => x.TeacherId == id);

            if (teacherDetail == null)
            {
                return NotFound(); 
            }
            return View(teacherDetail); 
        }
        public IActionResult Search(string search)
        {
            if (search == null) return NotFound();
            List<Teacher> model = _dbContext.Teachers.Where(p => p.Name.Contains(search) && p.IsDeleted == false).Include(x => x.socialMedias).Include(x => x.Position).Take(8).OrderByDescending(p => p.Id).ToList();
            return PartialView("_TeacherSearchPartial", model);
        }
    }
}
