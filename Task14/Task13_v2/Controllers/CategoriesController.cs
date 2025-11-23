using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Task13.DataAccess;
using Task13.Models;
using Task13_v2.Repositories;

namespace Task13.Controllers
{
    public class CategoriesController : Controller
    {
        ApplicationDbContext db = new();
        private Repository<Category> categoryRepo;
        CategoriesController()
        {
            this.categoryRepo = new Repository<Category>(db);
        }
        public async Task<IActionResult> CategoryList()
        {
            //var categories = db.categories.AsNoTracking().AsEnumerable();
            var categories = await categoryRepo.GetAsync(tracked: false);
            return View(categories);
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(string? Name)
        {
            if(Name is not null)
            {
                //db.categories.Add(new()
                //{
                //    Name = Name,
                //});
                await categoryRepo.CreateAsync(new Category { Name = Name });
                await categoryRepo.CommitAsync();
                //db.SaveChanges();
            }
            return RedirectToAction(nameof(CategoryList));
        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(int id)
        {
            //var cat = db.categories.FirstOrDefault(c => c.Id == id);
            var cat = await categoryRepo.GetOneAsync(c => c.Id == id);
            return View(cat);
        }
        [HttpPost]
        public async Task<IActionResult> EditCategory(int id, string Name)
        {
            //var cat = db.categories.FirstOrDefault(c => c.Id == id);
            var cat = await categoryRepo.GetOneAsync(c => c.Id == id);
            cat.Name = Name;
            //db.SaveChanges();
            await categoryRepo.CommitAsync();
            return RedirectToAction(nameof(CategoryList));
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            //var cat = db.categories.FirstOrDefault(c => c.Id == id);
            var cat = await categoryRepo.GetOneAsync( c => c.Id == id);
            //db.categories.Remove(cat);
            categoryRepo.Delete(cat);
            //db.SaveChanges();
            await categoryRepo.CommitAsync();
            return RedirectToAction(nameof(CategoryList));
        }
    }
}
