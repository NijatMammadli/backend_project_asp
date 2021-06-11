using FrontToBack_hw.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_project_asp.ViewComponents
{
    public class NoticeBoardViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public NoticeBoardViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notices = await _dbContext.NoticeBoards.OrderByDescending(x=> x.Date).ToListAsync();

            return View(notices); 
        }
    }
}
