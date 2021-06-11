using FrontToBack_hw.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_project_asp.ViewComponents
{
    public class WelComeAreaViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public WelComeAreaViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var welcomeArea = await _dbContext.WelcomeAreas.FirstOrDefaultAsync();

            return View(welcomeArea);
        }
    }
}
