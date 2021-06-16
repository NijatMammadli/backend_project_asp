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
    public class EventController : Controller
    {
        private readonly AppDbContext _dbcontext;

        public EventController(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public IActionResult Index(int? categoryId, int page=1)
        {
            List<Event> events = new List<Event>();
            if (categoryId == null)
            {
                ViewBag.PageCount = Decimal.Ceiling(_dbcontext.Events.Where(x => x.IsDeleted == false).Count() / 3);
                ViewBag.Page = page;

                return View(events);
            }
            else
            {

                IQueryable<EventCategory> eventCategories = _dbcontext.EventCategories.Where(x => x.CategoryId == categoryId).Include(x => x.Event);
                foreach (EventCategory ct in eventCategories)
                {
                    events.Add(ct.Event);
                }
                return View(events);
            }
                
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
            var categories = await _dbcontext.Categories.Include(x => x.EventCategories).ToListAsync();

            var eventViewModel = new EventViewModel
            {
                EventDetail = eventDetail,
                Categories = categories
            };

            return View(eventViewModel);
        }
    }
}
