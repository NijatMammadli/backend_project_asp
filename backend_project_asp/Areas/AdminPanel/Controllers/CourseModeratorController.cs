using backend_project_asp.Areas.AdminPanel.Utils;
using backend_project_asp.Data;
using backend_project_asp.Models;
using FrontToBack_hw.DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace backend_project_asp.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = RoleConstants.CourseModerator)]
    public class CourseModeratorController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public CourseModeratorController(AppDbContext dbContext,UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            User user = await _userManager.FindByNameAsync(User.Identity.Name);
            var courses = _dbContext.Courses.Where(x => x.IsDeleted == false && x.UserId==user.Id).Include(x => x.CourseDetail).Where(x => x.IsDeleted == false).ToList();
            return View(courses);
        }

        public IActionResult Update(int? id)
        {

            var categories = _dbContext.Categories.ToList();
            ViewBag.categories = categories;
            if (id == null)
            {
                return NotFound();
            }
            var courseExist = _dbContext.Courses.Include(x => x.CourseDetail).Include(x => x.CourseCategories).FirstOrDefault(x => x.Id == id);

            if (courseExist == null)
            {
                return NotFound();
            }

            return View(courseExist);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Course course, List<int?> categoryId)
        {
            var courseExist = _dbContext.Courses.Include(x => x.CourseDetail).Include(x => x.CourseCategories).FirstOrDefault(x => x.Id == id);

            var categories = _dbContext.Categories.ToList();
            ViewBag.categories = categories;

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id != course.Id)
            {
                return BadRequest();
            }




            if (course.Photo != null)
            {
                if (!course.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Select photo.");
                    return View();
                }

                if (!course.Photo.IsSizeAllowed(1024))
                {
                    ModelState.AddModelError("Photo", "Max size is 1024 kb.");
                    return View();
                }

                course.Image = await FileUtil.GenerateFileAsync(Path.Combine(Constants.ImageFolderPath, "course"), course.Photo);

            }



            List<CourseCategory> courseCategories = new List<CourseCategory>();
            foreach (var ctId in categoryId)
            {
                var courseCategory = new CourseCategory
                {
                    CategoryId = (int)ctId
                };

                courseCategories.Add(courseCategory);
            }


            courseExist.CourseCategories = courseCategories;
            courseExist.Name = course.Name;
            courseExist.Description = course.Description;
            courseExist.CourseDetail.AboutCourse = course.CourseDetail.AboutCourse;
            courseExist.CourseDetail.Certification = course.CourseDetail.Certification;
            courseExist.CourseDetail.ClassDuration = course.CourseDetail.ClassDuration;
            courseExist.CourseDetail.Duration = course.CourseDetail.Duration;
            courseExist.CourseDetail.Language = course.CourseDetail.Language;
            courseExist.CourseDetail.SkillLevel = course.CourseDetail.SkillLevel;
            courseExist.CourseDetail.Starts = course.CourseDetail.Starts;
            courseExist.CourseDetail.Students = course.CourseDetail.Students;
            courseExist.CourseDetail.Assesments = course.CourseDetail.Assesments;
            courseExist.CourseDetail.HowToApply = course.CourseDetail.HowToApply;

            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");


        }

    }
}
