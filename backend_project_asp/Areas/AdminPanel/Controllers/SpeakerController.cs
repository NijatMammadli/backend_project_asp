using backend_project_asp.Areas.AdminPanel.Utils;
using backend_project_asp.Models;
using FrontToBack_hw.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace backend_project_asp.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class SpeakerController : Controller
    {
        private readonly AppDbContext _dbContext;

        public SpeakerController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var speakers = _dbContext.Speakers.Where(x => x.IsDeleted == false).ToList(); 

            return View(speakers);
        }

        public IActionResult Create()
        {
          
            return View();
        }

        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Speaker speaker)
        {
           


            if (!ModelState.IsValid)
            {
                return View();
            }

            var isExist = _dbContext.Speakers.Any(x => x.Id == speaker.Id);
            if (isExist)
            {
                ModelState.AddModelError("", "This speaker is already exist");
                return View();
            }


            if (speaker.Photo == null)
            {
                ModelState.AddModelError("Photo", "Select photo.");
                return View();
            }

            if (!speaker.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Select photo.");
                return View();
            }

            if (!speaker.Photo.IsSizeAllowed(1024))
            {
                ModelState.AddModelError("Photo", "Max size is 1024 kb.");
                return View();
            }
            speaker.Image = await FileUtil.GenerateFileAsync(Path.Combine(Constants.ImageFolderPath, "event"), speaker.Photo);
            speaker.IsDeleted = false; 
          
          


            
            await _dbContext.Speakers.AddAsync(speaker);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {

          
            if (id == null)
            {
                return NotFound();
            }
            var speakerExist = await _dbContext.Speakers.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == id);

            if (speakerExist == null)
            {
                return NotFound();
            }

            return View(speakerExist);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Speaker speaker)
        {
           

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id == null)
            {
                return NotFound();
            }

            var speakerExist = await _dbContext.Speakers.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == id);

            if (speakerExist==null)
            {
                return NotFound();
            }


            if (id != speaker.Id)
            {
                return BadRequest();
            }




            if (speaker.Photo != null)
            {
                if (!speaker.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Select photo.");
                    return View();
                }

                if (!speaker.Photo.IsSizeAllowed(1024))
                {
                    ModelState.AddModelError("Photo", "Max size is 1024 kb.");
                    return View();
                }

                speaker.Image = await FileUtil.GenerateFileAsync(Path.Combine(Constants.ImageFolderPath, "event"), speaker.Photo);

            }
           else {
                speaker.Image = speakerExist.Image;
            }

            speakerExist.Image = speaker.Image;
            speakerExist.Name = speaker.Name;
            speakerExist.Position = speaker.Position;
            speakerExist.Company = speaker.Company; 
                    
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");


        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speakerExist = _dbContext.Speakers.Where(x => x.Id == id).FirstOrDefault();
            if (speakerExist == null)
            {
                return NotFound();
            }


            return View(speakerExist);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speakerExist = _dbContext.Speakers.Where(x => x.Id == id).FirstOrDefault();
            if (speakerExist == null)
            {
                return NotFound();
            }

            return View(speakerExist);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeleteSpeaker(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var speakerExist = _dbContext.Speakers.Where(x => x.Id == id).FirstOrDefault();


          

            if (speakerExist == null)
            {
                return NotFound();
            }
            speakerExist.IsDeleted = true;
           

            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
