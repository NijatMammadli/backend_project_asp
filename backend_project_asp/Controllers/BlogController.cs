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
    public class BlogController : Controller
    {
        private readonly AppDbContext _dbcontext;

        public BlogController(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public  IActionResult Index(int? categoryId, int page=1)
        {
            List<Blog> blogs = new List<Blog>();
            if (categoryId == null)
            {
                ViewBag.PageCount = Math.Ceiling((decimal)_dbcontext.Blogs.Where(x => x.IsDeleted == false).Count() / 3);
                ViewBag.Page = page;

                return View(blogs);
            }
            else
            {
                IQueryable<BlogCategory> blogCategories = _dbcontext.BlogCategories.Where(x => x.CategoryId == categoryId).Include(x => x.Blog);
                foreach (BlogCategory bct in blogCategories)
                {
                    blogs.Add(bct.Blog);
                }
                return View(blogs);

            }

        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound(); 
            }
            var blogDetail = await _dbcontext.BlogDetails.Include(x => x.Blog).FirstOrDefaultAsync(x => x.BlogId == id);
            if (blogDetail == null)
            {
                return NotFound(); 
            }

            var categories = await _dbcontext.Categories.Include(x => x.BlogCategories).ToListAsync();
            var blogViewModel = new BlogViewModel
            {
                BlogDetail = blogDetail,
                Categories = categories
            };

            return View(blogViewModel); 
        }

        public IActionResult Search(string search)
        {
            if (search == null) return NotFound();
            List<Blog> model = _dbcontext.Blogs.Where(p => p.Name.Contains(search) && p.IsDeleted == false).Take(8).OrderByDescending(p => p.Id).ToList();
            return PartialView("_BlogSearchPartial", model);
        }
    }
}
