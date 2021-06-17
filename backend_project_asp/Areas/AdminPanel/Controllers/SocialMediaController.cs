using backend_project_asp.Models;
using FrontToBack_hw.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_project_asp.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class SocialMediaController : Controller
    {
        private readonly AppDbContext _dbContext;

        public SocialMediaController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var teachers = await _dbContext.Teachers.Where(s => s.IsDeleted == false).Include(x => x.socialMedias.Where(s=>s.IsDeleted==false)).ToListAsync();

            return View(teachers);
        }
        public async Task<IActionResult> Create()
        {
            var teachers = await _dbContext.Teachers.ToListAsync();
            var allSocialMedias = await _dbContext.AllSocialMedias.ToListAsync();
            ViewBag.teachers = teachers;
            ViewBag.allSocialMedias = allSocialMedias;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SocialMedia  socialMedia,int? teacherId)
        {
            var teachers = await _dbContext.Teachers.ToListAsync();
            var allSocialMedias = await _dbContext.AllSocialMedias.ToListAsync();
            ViewBag.teachers = teachers;
            ViewBag.allSocialMedias = allSocialMedias;
            if (teacherId == null)
            {
                ModelState.AddModelError("", "Select photo.");
                return View();
            }
            socialMedia.TeacherId = (int)teacherId;

            await _dbContext.SocialMedias.AddAsync(socialMedia);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Update(int? id)
        {
            var teachers = await _dbContext.Teachers.ToListAsync();
            var allSocialMedias = await _dbContext.AllSocialMedias.ToListAsync();
            ViewBag.teachers = teachers;
            ViewBag.allSocialMedias = allSocialMedias;

            if (id == null)
            {
                return NotFound();
            }

            var socialMediaExist = await _dbContext.SocialMedias.FirstOrDefaultAsync(x => x.Id == id);

            if (socialMediaExist == null)
            {
                return NotFound();
            }


            return View(socialMediaExist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, SocialMedia socialMedia, int? teacherId)
        {
            var teachers = await _dbContext.Teachers.ToListAsync();
            var allSocialMedias = await _dbContext.AllSocialMedias.ToListAsync();
            ViewBag.teachers = teachers;
            ViewBag.allSocialMedias = allSocialMedias;

            if (id == null)
            {
                return NotFound();
            }
            
            var socialMediaExist = await _dbContext.SocialMedias.FirstOrDefaultAsync(x => x.Id == id);
            if (teacherId == null)
            {
                ModelState.AddModelError("", "Select photo.");
                return View();
            }
            if (socialMediaExist == null)
            {
                return NotFound();
            }

            socialMediaExist.Icon = socialMedia.Icon;
            socialMediaExist.Link = socialMedia.Link;
            socialMediaExist.TeacherId = (int)teacherId;

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
        

            if (id == null)
            {
                return NotFound();
            }

            var socialMediaExist = await _dbContext.SocialMedias.Where(x => x.Id == id).FirstOrDefaultAsync();
            
            var teacher = await _dbContext.Teachers.Where(x => x.Id ==socialMediaExist.TeacherId).FirstOrDefaultAsync();
            ViewBag.teacher = teacher; 
            return View(socialMediaExist);

        }

        public async Task<IActionResult> Delete(int? id)
        {


            if (id == null)
            {
                return NotFound();
            }

            var socialMediaExist = await _dbContext.SocialMedias.Where(x => x.Id == id).FirstOrDefaultAsync();

            var teacher = await _dbContext.Teachers.Where(x => x.Id == socialMediaExist.TeacherId).FirstOrDefaultAsync();
            ViewBag.teacher = teacher;
            return View(socialMediaExist);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteLink(int? id)
        {


            if (id == null)
            {
                return NotFound();
            }

            var socialMediaExist = await _dbContext.SocialMedias.Where(x => x.Id == id).FirstOrDefaultAsync();

            var teacher = await _dbContext.Teachers.Where(x => x.Id == socialMediaExist.TeacherId).FirstOrDefaultAsync();
            ViewBag.teacher = teacher;

            socialMediaExist.IsDeleted = true;

            await _dbContext.SaveChangesAsync(); 

            return RedirectToAction("Index");

        }
    }
}
