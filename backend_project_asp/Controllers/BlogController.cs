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
        public async Task<IActionResult> Index(int page=1)
        {
            ViewBag.PageCount = Decimal.Ceiling(_dbcontext.Blogs.Where(x => x.IsDeleted == false).Count() / 3);
            ViewBag.Page = page;
            var blogs = await _dbcontext.Blogs.OrderByDescending(x => x.Id).Skip((page-1)*3).Take(3).ToListAsync(); 
            return View(blogs);
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

            return View(blogDetail); 
        }
    }
}
