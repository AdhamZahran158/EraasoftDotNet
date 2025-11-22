using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task13.DataAccess;
using Task13.Models;
using Task13_v2.Repositories;

namespace Task13.Controllers
{
    public class CategoriesController : Controller
    {
        ApplicationDbContext db = new();
        private readonly Repository<Category> categoryRepo = new Repository<Category>( new ApplicationDbContext());
        public IActionResult CategoryList()
        {
            var categories = db.categories.AsNoTracking().AsEnumerable();
            return View(categories);
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(string? Name)
        {
            if(Name is not null)
            {
                db.categories.Add(new()
                {
                    Name = Name,
                });
                db.SaveChanges();
            }
            return RedirectToAction(nameof(CategoryList));
        }

        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var cat = db.categories.FirstOrDefault(c => c.Id == id);
            return View(cat);
        }
        [HttpPost]
        public IActionResult EditCategory(int id, string Name)
        {
            var cat = db.categories.FirstOrDefault(c => c.Id == id);
            cat.Name = Name;
            db.SaveChanges();
            return RedirectToAction(nameof(CategoryList));
        }

        public IActionResult DeleteCategory(int id)
        {
            var cat = db.categories.FirstOrDefault(c => c.Id == id);
            db.categories.Remove(cat);
            db.SaveChanges();
            return RedirectToAction(nameof(CategoryList));
        }
    }
}
