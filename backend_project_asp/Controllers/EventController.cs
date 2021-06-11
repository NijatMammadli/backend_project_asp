using FrontToBack_hw.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_project_asp.Controllers
{
    public class EventController : Controller
    {
        private readonly AppDbContext _dbcontext;

        public EventController(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<IActionResult> Index(int page=1)
        {
            ViewBag.PageCount = Decimal.Ceiling(_dbcontext.Events.Where(x=> x.IsDeleted==false).Count()/3);
            ViewBag.Page = page;
            
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound(); 
            }

            var eventDetail = await _dbcontext.EventDetails.Include(x => x.Event).ThenInclude(x => x.EventSpeakers).ThenInclude(x => x.Speaker).FirstOrDefaultAsync(x => x.EventId == id);
            if (eventDetail == null)
            {
                return NotFound(); 
            }
            return View(eventDetail);
        }
    }
}
