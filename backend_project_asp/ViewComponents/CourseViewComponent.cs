using FrontToBack_hw.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_project_asp.ViewComponents
{
    public class CourseViewComponent:ViewComponent
    {

        private readonly AppDbContext _dbContext;

        public CourseViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(int count, int? page)
        {
          
           

            if (page == null)
            {
                var courses = await _dbContext.Courses.OrderByDescending(x => x.Id).Take(count).ToListAsync();
                return View(courses);
            }
            else
            {
                var courses = await _dbContext.Courses.OrderByDescending(x => x.Id).Skip((int)(page - 1) * count).Take(count).ToListAsync();
                return View(courses);

            }
        }
    }
}
