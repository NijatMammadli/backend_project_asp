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

    public class PositionController : Controller
    {
        private readonly AppDbContext _dbContext;

        public PositionController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var positions = _dbContext.Positions.Where(x=>x.IsDeleted==false).ToList();

            return View(positions);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Position position)
        {



            if (!ModelState.IsValid)
            {
                return View();
            }

            var isExist = _dbContext.Categories.Any(x => x.Id == position.Id);
            if (isExist)
            {
                ModelState.AddModelError("", "This position is already exist");
                return View();
            }

            position.IsDeleted = false;
            await _dbContext.Positions.AddAsync(position);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {


            if (id == null)
            {
                return NotFound();
            }
            var positionExist = await _dbContext.Positions.FirstOrDefaultAsync(x => x.Id == id);

            if (positionExist == null)
            {
                return NotFound();
            }

            return View(positionExist);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Position position)
        {


            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id == null)
            {
                return NotFound();
            }

            var positionExist = await _dbContext.Positions.FirstOrDefaultAsync(x => x.Id == id);

            if (positionExist == null)
            {
                return NotFound();
            }


            if (id != position.Id)
            {
                return BadRequest();
            }


            positionExist.PositionName = position.PositionName;

            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");


        }

        public async Task<IActionResult> DetailsAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var positionExist = await _dbContext.Positions.FirstOrDefaultAsync(x => x.Id == id);

            if (positionExist == null)
            {
                return NotFound();
            }


            return View(positionExist);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var positionExist = _dbContext.Positions.FirstOrDefault(x => x.Id == id);


            if (positionExist == null)
            {
                return NotFound();
            }

            return View(positionExist);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeletePosition(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var positionExist = _dbContext.Positions.Where(x => x.Id == id).FirstOrDefault();

            if (positionExist == null)
            {
                return NotFound();
            }



            positionExist.IsDeleted = true; 
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
