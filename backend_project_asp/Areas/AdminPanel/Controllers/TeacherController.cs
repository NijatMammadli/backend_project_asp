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
    public class TeacherController : Controller
    {
        private readonly AppDbContext _dbContext;

        public TeacherController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            var teachers = await _dbContext.Teachers.Where(x => x.IsDeleted == false).Include(x => x.Position).Include(x=>x.TecaherDetail).OrderByDescending(x => x.Id).ToListAsync();

            return View(teachers);
        }

        public IActionResult Create()
        {            
            var positions = _dbContext.Positions.Where(x => x.IsDeleted == false).ToList();
            ViewBag.positions = positions;
            
            return View();
        }
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Teacher Teacher, int? positionId) {

            var positions = _dbContext.Positions.Where(x => x.IsDeleted == false).ToList();
            ViewBag.positions = positions;


            if (!ModelState.IsValid)
            {
                return View();
            }

            var isExist = _dbContext.Teachers.Any(x => x.Id==Teacher.Id);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This Teacher is already exist");
                return View();
            }


            if (Teacher.Photo == null)
            {
                ModelState.AddModelError("Photo", "Select photo.");
                return View();
            }

            if (!Teacher.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Select photo.");
                return View();
            }

            if (!Teacher.Photo.IsSizeAllowed(1024))
            {
                ModelState.AddModelError("Photo", "Max size is 1024 kb.");
                return View();
            }
            Teacher.Image = await FileUtil.GenerateFileAsync(Path.Combine(Constants.ImageFolderPath, "teacher"), Teacher.Photo);
            if (positionId == null)
            {
                ModelState.AddModelError("", "Please select at least one spiker");
                return View();
            }

            Teacher.PositionId =(int) positionId; 
          
         


          
            await _dbContext.Teachers.AddAsync(Teacher);
            await _dbContext.SaveChangesAsync();
           
            return RedirectToAction("Index");
        }

        public IActionResult Update(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

         

            var isExist = _dbContext.Teachers.Where(x=>x.IsDeleted==false).Include(x => x.Position).Include(x => x.TecaherDetail).FirstOrDefault(x => x.Id ==id);
            if (isExist==null)
            {
                return NotFound(); 
            }
            var positions = _dbContext.Positions.Where(x => x.IsDeleted == false).ToList();
            ViewBag.positions = positions;

            return View(isExist);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Teacher Teacher, int? positionId)
        {
            var positions = _dbContext.Positions.Where(x => x.IsDeleted == false).ToList();
            ViewBag.positions = positions;

            if (id == null)
            {
                return BadRequest();
            }

            var teacherExist = _dbContext.Teachers.Where(x=>x.IsDeleted==false).Include(x => x.Position).Include(x => x.TecaherDetail).FirstOrDefault(x => x.Id ==id);

            if (teacherExist == null)
            {
                return BadRequest();
            }




            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id != Teacher.Id)
            {
                return BadRequest();
            }


            if (positionId == null)
            {
                ModelState.AddModelError("", "Select position.");
                return View();
            }

            if (Teacher.Photo != null)
            {
                if (!Teacher.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Select photo.");
                    return View();
                }

                if (!Teacher.Photo.IsSizeAllowed(1024))
                {
                    ModelState.AddModelError("Photo", "Max size is 1024 kb.");
                    return View();
                }

                teacherExist.Image = await FileUtil.GenerateFileAsync(Path.Combine(Constants.ImageFolderPath, "teacher"), Teacher.Photo);

            }

            teacherExist.Name = Teacher.Name;
            teacherExist.Position.Id = (int) positionId;
            teacherExist.TecaherDetail.AboutMe= Teacher.TecaherDetail.AboutMe;
            teacherExist.TecaherDetail.Degree= Teacher.TecaherDetail.Degree;
            teacherExist.TecaherDetail.Experience= Teacher.TecaherDetail.Experience;
            teacherExist.TecaherDetail.Hobbies= Teacher.TecaherDetail.Hobbies;
            teacherExist.TecaherDetail.Faculty= Teacher.TecaherDetail.Faculty;
            teacherExist.TecaherDetail.Mail= Teacher.TecaherDetail.Mail;
            teacherExist.TecaherDetail.Phone= Teacher.TecaherDetail.Phone;
            teacherExist.TecaherDetail.Skype= Teacher.TecaherDetail.Skype;
            teacherExist.TecaherDetail.Language= Teacher.TecaherDetail.Language;
            teacherExist.TecaherDetail.TeamLeader= Teacher.TecaherDetail.TeamLeader;
            teacherExist.TecaherDetail.Development= Teacher.TecaherDetail.Development;
            teacherExist.TecaherDetail.Design= Teacher.TecaherDetail.Design;
            teacherExist.TecaherDetail.Innovation= Teacher.TecaherDetail.Innovation;
            teacherExist.TecaherDetail.Communication= Teacher.TecaherDetail.Communication;
            teacherExist.IsDeleted = false;
            teacherExist.TecaherDetail.IsDeleted = false;
                           

            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");


        }

     

        public IActionResult Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var teacherExist = _dbContext.Teachers.Include(x => x.TecaherDetail).Include(x => x.Position).FirstOrDefault(x => x.Id == id);
            if (teacherExist == null)
            {
                return NotFound();
            }
       

            return View(teacherExist);
        }

        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var teacherExist = _dbContext.Teachers.Include(x => x.TecaherDetail).Include(x => x.Position).FirstOrDefault(x => x.Id == id);
            if (teacherExist == null)
            {
                return NotFound();
            }
            

            return View(teacherExist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeleteTeacher(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var teacherExist = _dbContext.Teachers.Include(x => x.TecaherDetail).Include(x => x.Position).FirstOrDefault(x => x.Id == id);

            if (teacherExist == null)
            {
                return NotFound();
            }
            teacherExist.IsDeleted = true;
            teacherExist.TecaherDetail.IsDeleted = true;

            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
