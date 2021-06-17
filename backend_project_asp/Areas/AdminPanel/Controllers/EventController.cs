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

    public class EventController : Controller
    {
        private readonly AppDbContext _dbContext;

        public EventController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var events = _dbContext.Events.Where(x => x.IsDeleted == false).Include(x => x.EventDetail).ToList(); 


            return View(events);
        }

        public IActionResult Create()
        {
            var categories = _dbContext.Categories.ToList();
            var speakers = _dbContext.Speakers.Where(x=>x.IsDeleted==false).ToList();
            ViewBag.categories = categories;
            ViewBag.Spikers = speakers;
            return View();
        }

        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event Event, List<int?> categoriesId, List<int?> spikersId)
        {
            var categories = _dbContext.Categories.ToList();
            var speakers = _dbContext.Speakers.Where(x => x.IsDeleted == false).ToList();
            ViewBag.categories = categories;
            ViewBag.Spikers = speakers;


            if (!ModelState.IsValid)
            {
                return View();
            }

            var isExist = _dbContext.Events.Any(x => x.Name.ToLower() == Event.Name.ToLower());
            if (isExist)
            {
                ModelState.AddModelError("Name", "This Event name already exist");
                return View();
            }


            if (Event.Photo == null)
            {
                ModelState.AddModelError("Photo", "Select photo.");
                return View();
            }

            if (!Event.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Select photo.");
                return View();
            }

            if (!Event.Photo.IsSizeAllowed(1024))
            {
                ModelState.AddModelError("Photo", "Max size is 1024 kb.");
                return View();
            }
            Event.Image = await FileUtil.GenerateFileAsync(Path.Combine(Constants.ImageFolderPath, "Event"), Event.Photo);
            if (spikersId.Count == 0)
            {
                ModelState.AddModelError("", "Please select at least one spiker");
                return View();
            }
            
            if (categoriesId.Count == 0)
            {
                ModelState.AddModelError("", "Please select at least one category");
                return View();
            }
            List<EventCategory> EventCategories = new List<EventCategory>();
            List<EventSpeaker> eventSpeakers = new List<EventSpeaker>();
            foreach (var sId in spikersId)
            {
                var eventSpeaker = new EventSpeaker
                {
                    SpeakerId = (int)sId
                };

                eventSpeakers.Add(eventSpeaker);
            }
            foreach (var ctId in categoriesId)
            {
                var EventCategory = new EventCategory
                {
                    CategoryId = (int)ctId
                };

                EventCategories.Add(EventCategory);
            }


            Event.EventSpeakers = eventSpeakers;
            Event.EventCategories = EventCategories;
            await _dbContext.Events.AddAsync(Event);
            await _dbContext.SaveChangesAsync();
            //           Subscribe
            await SubscribeEmailAsync(Event.Id);
            return RedirectToAction("Index");
        }
        public async Task SubscribeEmailAsync(int? eventId)
        {
            List<Subscribe> subscribes = _dbContext.Subscribes.ToList();
            string msgsubject = "New event is planned";
            string url = "https://localhost:44342/Event/Details/" + eventId;
            string message = $"<a href={url}>link to new event, please click to view details</a>";
            foreach (var subscriber in subscribes)
            {
                await Helper.SendMessage(msgsubject, message, subscriber.Email);
            }
        }


        public IActionResult Update(int? id)
        {
            var categories = _dbContext.Categories.ToList();
            var speakers = _dbContext.Speakers.Where(x => x.IsDeleted == false).ToList();
            ViewBag.categories = categories;
            ViewBag.Spikers = speakers;
            if (id == null)
            {
                return NotFound();
            }

            var isExistEvent = _dbContext.Events.Where(x => x.IsDeleted == false).Include(x=>x.EventDetail).Include(x=>x.EventCategories).Include(x=>x.EventSpeakers).FirstOrDefault(x => x.Id == id);
            if (isExistEvent == null)
            {
                return NotFound(); 
            }
           
            return View(isExistEvent);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Event Event, List<int?> categoryId, List<int?> speakerId)
        {
            var categories = _dbContext.Categories.ToList();
            var speakers = _dbContext.Speakers.Where(x => x.IsDeleted == false).ToList();
            ViewBag.categories = categories;
            ViewBag.Spikers = speakers;
            if (id == null)
            {
                return BadRequest();
            }

            var eventExist = _dbContext.Events.Include(x => x.EventDetail).Include(x => x.EventCategories).Include(x=>x.EventSpeakers).FirstOrDefault(x => x.Id == id);
            if (eventExist == null)
            {
                return BadRequest();
            }

           
          

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id != Event.Id)
            {
                return BadRequest();
            }




            if (Event.Photo != null)
            {
                if (!Event.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Select photo.");
                    return View();
                }

                if (!Event.Photo.IsSizeAllowed(1024))
                {
                    ModelState.AddModelError("Photo", "Max size is 1024 kb.");
                    return View();
                }

                Event.Image = await FileUtil.GenerateFileAsync(Path.Combine(Constants.ImageFolderPath, "event"), Event.Photo);

            }



            List<EventCategory> eventCategories = new List<EventCategory>();
            List<EventSpeaker> eventSpeakers = new List<EventSpeaker>();

            foreach (var ctId in categoryId)
            {
                var eventCategory = new EventCategory
                {
                    CategoryId = (int)ctId
                };

                eventCategories.Add(eventCategory);
            }

            foreach (var spId in speakerId)
            {
                var eventSpeaker = new EventSpeaker
                {
                    SpeakerId = (int)spId
                };

                eventSpeakers.Add(eventSpeaker);
            }


            eventExist.EventCategories = eventCategories;
            eventExist.EventSpeakers = eventSpeakers;
            eventExist.Name = Event.Name;
            eventExist.StartTime = Event.StartTime;
            eventExist.EndTime = Event.EndTime;
            eventExist.Venue = Event.Venue;
            eventExist.EventDetail.Description = Event.EventDetail.Description;
            eventExist.EventDetail.DetailedVenue = Event.EventDetail.DetailedVenue;

            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");


        }

        public IActionResult Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var eventExist = _dbContext.Events.Include(x => x.EventDetail).Include(x => x.EventCategories).Include(x => x.EventSpeakers).FirstOrDefault(x => x.Id == id);
            if (eventExist == null)
            {
                return NotFound();
            }
            var categories = _dbContext.Categories.ToList();
            var speakers = _dbContext.Speakers.Where(x => x.IsDeleted == false).ToList();
            ViewBag.categories = categories;
            ViewBag.Spikers = speakers;

            return View(eventExist);
        }

        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var eventExist = _dbContext.Events.Include(x => x.EventDetail).Include(x => x.EventCategories).Include(x=>x.EventSpeakers).FirstOrDefault(x => x.Id == id);
            if (eventExist == null)
            {
                return NotFound();
            }
            var categories = _dbContext.Categories.ToList();
            var speakers = _dbContext.Speakers.Where(x => x.IsDeleted == false).ToList();
            ViewBag.categories = categories;
            ViewBag.Spikers = speakers;

            return View(eventExist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeleteEvent(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var eventExist = _dbContext.Events.Include(x => x.EventDetail).Include(x => x.EventCategories).Include(x => x.EventSpeakers).FirstOrDefault(x => x.Id == id);


            var categories = _dbContext.Categories.ToList();
            var speakers = _dbContext.Speakers.Where(x => x.IsDeleted == false).ToList();
            ViewBag.categories = categories;
            ViewBag.Spikers = speakers;

            if (eventExist == null)
            {
                return NotFound();
            }
            eventExist.IsDeleted = true;
            eventExist.EventDetail.IsDeleted = true;

            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
