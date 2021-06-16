using backend_project_asp.Areas.AdminPanel.Utils;
using backend_project_asp.Models;
using FrontToBack_hw.DataAccessLayer;
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
    public class CourseController : Controller
    {
        private readonly AppDbContext _dbContext;

        public CourseController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {

            var courses = _dbContext.Courses.Where(x=>x.IsDeleted==false).Include(x => x.CourseDetail).Where(x=>x.IsDeleted==false).ToList();
            return View(courses);
        }

        public IActionResult Create()
        {
            var categories = _dbContext.Categories.ToList();
            ViewBag.categories = categories;
            return View();
        }

        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course, List<int?> categoryId)
        {
            var categories = _dbContext.Categories.ToList();
            ViewBag.categories = categories;


            if (!ModelState.IsValid)
            {
                return View();
            }

            var isExist = _dbContext.Courses.Any(x => x.Name.ToLower() == course.Name.ToLower());
            if (isExist)
            {
                ModelState.AddModelError("Name", "This Course name already exist");
                return View(); 
            }


            if (course.Photo == null)
            {
                ModelState.AddModelError("Photo", "Select photo.");
                return View();
            }

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

            List<CourseCategory> courseCategories = new List<CourseCategory>();
            if (categoryId.Count == 0)
            {
                ModelState.AddModelError("categoryId", "Please select at least one category");
                return View(); 
            }
            foreach (var ctId in categoryId)
            {
                var courseCategory = new CourseCategory
                {
                    CategoryId =(int) ctId
                };

                courseCategories.Add(courseCategory);
            }


            course.CourseCategories = courseCategories;
            await _dbContext.Courses.AddAsync(course);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
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
        public async Task<IActionResult> Update(int? id,Course course, List<int?> categoryId)
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

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var courseExist = _dbContext.Courses.Include(x => x.CourseDetail).Include(x => x.CourseCategories).FirstOrDefault(x => x.Id == id);

            var categories = _dbContext.Categories.ToList();
            ViewBag.categories = categories;

            if (courseExist == null)
            {
                return NotFound(); 
            }


            return View(courseExist);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseExist = _dbContext.Courses.Include(x => x.CourseDetail).Include(x => x.CourseCategories).FirstOrDefault(x => x.Id == id);
            if (courseExist == null)
            {
                return NotFound();
            }
            var categories = _dbContext.Categories.ToList();
            ViewBag.categories = categories;

            return View(courseExist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeleteCourse(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var courseExist = _dbContext.Courses.Include(x => x.CourseDetail).Include(x => x.CourseCategories).FirstOrDefault(x => x.Id == id);

            var categories = _dbContext.Categories.ToList();
            ViewBag.categories = categories;

            if (courseExist == null)
            {
                return NotFound();
            }
            courseExist.IsDeleted = true;
            courseExist.CourseDetail.IsDeleted = true; 

            _dbContext.SaveChanges(); 
            return RedirectToAction("Index"); 
        }
    }
}
