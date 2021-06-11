using FrontToBack_hw.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_project_asp.ViewComponents
{
    public class EventViewComponent:ViewComponent
    {
        private readonly AppDbContext _dbcontext;

        public EventViewComponent(AppDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(int count, int? page)
        {
            if (page == null)
            {
                var events = await _dbcontext.Events.OrderByDescending(x => x.Id).Take(count).ToListAsync();
                return View(events);

            }
            else
            {
                var events = await _dbcontext.Events.OrderByDescending(x => x.Id).Skip((int)(page - 1) * count).Take(count).ToListAsync();
                return View(events);


            }
        }
    }
}
