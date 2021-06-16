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

    public class BlogController : Controller
    {
        private readonly AppDbContext _dbContext;

        public BlogController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var blogs = _dbContext.Blogs.Where(x => x.IsDeleted == false).Include(x => x.BlogDetail).Where(x => x.IsDeleted == false).ToList();
            return View(blogs);
        }

        public IActionResult Create()
        {
            var categories = _dbContext.Categories.ToList();
            ViewBag.categories = categories;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog, List<int?> categoryId)
        {
            var categories = _dbContext.Categories.ToList();
            ViewBag.categories = categories;


            if (!ModelState.IsValid)
            {
                return View();
            }

            var isExist = _dbContext.Blogs.Any(x => x.Name.ToLower() == blog.Name.ToLower());
            if (isExist)
            {
                ModelState.AddModelError("Name", "This Blog name already exist");
                return View();
            }


            if (blog.Photo == null)
            {
                ModelState.AddModelError("Photo", "Select photo.");
                return View();
            }

            if (!blog.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Select photo.");
                return View();
            }

            if (!blog.Photo.IsSizeAllowed(1024))
            {
                ModelState.AddModelError("Photo", "Max size is 1024 kb.");
                return View();
            }
            blog.Image = await FileUtil.GenerateFileAsync(Path.Combine(Constants.ImageFolderPath, "blog"), blog.Photo);

            List<BlogCategory> blogCategories = new List<BlogCategory>();
            if (categoryId.Count == 0)
            {
                ModelState.AddModelError("categoryId", "Please select at least one category");
                return View();
            }
            foreach (var bId in categoryId)
            {
                var blogCategory = new BlogCategory
                {
                    CategoryId = (int)bId
                };

                blogCategories.Add(blogCategory);
            }


            blog.BlogCategories = blogCategories;
            await _dbContext.Blogs.AddAsync(blog);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var blogExist = _dbContext.Blogs.Include(x => x.BlogDetail).Include(x => x.BlogCategories).FirstOrDefault(x => x.Id == id);

            var categories = _dbContext.Categories.ToList();
            ViewBag.categories = categories;

            if (blogExist == null)
            {
                return NotFound();
            }


            return View(blogExist);
        }

        public IActionResult Update(int? id)
        {

            var categories = _dbContext.Categories.ToList();
            ViewBag.categories = categories;
            if (id == null)
            {
                return NotFound();
            }
            var blogExist = _dbContext.Blogs.Include(x => x.BlogDetail).Include(x => x.BlogCategories).FirstOrDefault(x => x.Id == id);

            if (blogExist == null)
            {
                return NotFound();
            }

            return View(blogExist);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Blog blog, List<int?> categoryId)
        {
            var blogExist = _dbContext.Blogs.Include(x => x.BlogDetail).Include(x => x.BlogCategories).FirstOrDefault(x => x.Id == id);

            var categories = _dbContext.Categories.ToList();
            ViewBag.categories = categories;

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id != blog.Id)
            {
                return BadRequest();
            }




            if (blog.Photo != null)
            {
                if (!blog.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Select photo.");
                    return View();
                }

                if (!blog.Photo.IsSizeAllowed(1024))
                {
                    ModelState.AddModelError("Photo", "Max size is 1024 kb.");
                    return View();
                }

                blog.Image = await FileUtil.GenerateFileAsync(Path.Combine(Constants.ImageFolderPath, "blog"), blog.Photo);

            }



            List<BlogCategory> blogCategories = new List<BlogCategory>();
            foreach (var bId in categoryId)
            {
                var blogCategory = new BlogCategory
                {
                    CategoryId = (int)bId
                };

                blogCategories.Add(blogCategory);
            }


            blogExist.BlogCategories = blog.BlogCategories;
            blogExist.Name = blog.Name;
            blogExist.Date = blog.Date;
            blogExist.Author = blog.Author;
            blogExist.CommentCount = blog.CommentCount;
            blogExist.BlogDetail.Description = blog.BlogDetail.Description;
         

            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");


        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogExist = _dbContext.Blogs.Include(x => x.BlogDetail).Include(x => x.BlogCategories).FirstOrDefault(x => x.Id == id);
            if (blogExist == null)
            {
                return NotFound();
            }
            var categories = _dbContext.Categories.ToList();
            ViewBag.categories = categories;

            return View(blogExist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeleteBlog(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var blogExist = _dbContext.Blogs.Include(x => x.BlogDetail).Include(x => x.BlogCategories).FirstOrDefault(x => x.Id == id);

            var categories = _dbContext.Categories.ToList();
            ViewBag.categories = categories;

            if (blogExist == null)
            {
                return NotFound();
            }
            blogExist.IsDeleted = true;
            blogExist.BlogDetail.IsDeleted = true;

            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
