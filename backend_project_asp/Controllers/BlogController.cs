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
        public  IActionResult Index(int page=1)
        {
            ViewBag.PageCount = Math.Ceiling((decimal)_dbcontext.Blogs.Where(x => x.IsDeleted == false).Count() / 3);
            ViewBag.Page = page;
            
            return View();
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
