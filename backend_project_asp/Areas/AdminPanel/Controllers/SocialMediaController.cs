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
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(SocialMedia  socialMedia,int? teacherId)
        {
            var teachers = await _dbContext.Teachers.ToListAsync();
            var allSocialMedias = await _dbContext.AllSocialMedias.ToListAsync();
            ViewBag.teachers = teachers;
            ViewBag.allSocialMedias = allSocialMedias;

            socialMedia.TeacherId = (int)teacherId;
            return View();
        }
    }
}
