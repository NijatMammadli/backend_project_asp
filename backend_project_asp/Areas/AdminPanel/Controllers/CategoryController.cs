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
    public class CategoryController : Controller
    {

        private readonly AppDbContext _dbContext;

        public CategoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var categories = _dbContext.Categories.ToList();

            return View(categories);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {



            if (!ModelState.IsValid)
            {
                return View();
            }

            var isExist = _dbContext.Categories.Any(x => x.Id == category.Id);
            if (isExist)
            {
                ModelState.AddModelError("", "This category is already exist");
                return View();
            }
                   

            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {


            if (id == null)
            {
                return NotFound();
            }
            var categoryExist = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (categoryExist == null)
            {
                return NotFound();
            }

            return View(categoryExist);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Category category)
        {


            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id == null)
            {
                return NotFound();
            }

            var categoryExist = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (categoryExist == null)
            {
                return NotFound();
            }


            if (id != category.Id)
            {
                return BadRequest();
            }


            categoryExist.Name = category.Name;

            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");


        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryExist = _dbContext.Categories.Where(x => x.Id == id).FirstOrDefault();
            if (categoryExist == null)
            {
                return NotFound();
            }


            return View(categoryExist);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryExist = _dbContext.Categories.Where(x => x.Id == id).FirstOrDefault();

            if (categoryExist == null)
            {
                return NotFound();
            }

            return View(categoryExist);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeleteCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var categoryExist = _dbContext.Categories.Where(x => x.Id == id).FirstOrDefault();

            if (categoryExist == null)
            {
                return NotFound();
            }



            _dbContext.Categories.Remove(categoryExist);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
