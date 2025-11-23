using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Task13.DataAccess;
using Task13.Models;
using Task13_v2.Repositories;

namespace Task13.Controllers
{
    public class CinemaController : Controller
    {
        ApplicationDbContext db = new();
        Repository<Cinema> cinemaRepo;
        public CinemaController()
        {
            this.cinemaRepo = new(db);
        }

        public async Task<IActionResult> CinemaList()
        {
            //var cinemas = db.cinema.AsEnumerable();
            var cinemas = await cinemaRepo.GetAsync(tracked: false);
            return View(cinemas);
        }
        [HttpGet]
        public IActionResult AddCinema()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCinema(string? Name, IFormFile? Img)
        {
            string fileName = "";
            if(Img is not  null && Img.Length>0)
            {
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(Img.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\CinemaImg", fileName);
                if(!System.IO.File.Exists(filePath))
                    System.IO.File.Create(filePath);
            }
            if (Name is not null && fileName != "")
            {
                //db.cinema.Add(new()
                //{
                //    Name = Name,
                //    Img = fileName
                //});
                await cinemaRepo.CreateAsync(new()
                {
                    Name = Name,
                    Img = fileName
                });
                //db.SaveChanges();
                await cinemaRepo.CommitAsync();
            }
            return RedirectToAction(nameof(CinemaList));
        }

        [HttpGet]
        public async Task<IActionResult> EditCinema(int id)
        {
            //var cinema = db.cinema.FirstOrDefault(c => c.Id == id);
            var cinema = await cinemaRepo.GetOneAsync(c =>  c.Id == id);
            return View(cinema);
        }
        [HttpPost]
        public async Task<IActionResult> EditCinema(int id, string Name, IFormFile Img)
        {
            //var cat = db.cinema.FirstOrDefault(c => c.Id == id);
            var cat = await cinemaRepo.GetOneAsync(c => c.Id == id);

            if(Img is not null && Img.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(Img.FileName);
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\CinemaImg", cat.Img);
                var newPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\CinemaImg", fileName);
                using (var stream = new FileStream(newPath, FileMode.Create))
                {
                    Img.CopyTo(stream);
                }

                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                cat.Img = fileName;
            }
            cat.Name = Name;
            await cinemaRepo.CommitAsync();
            //db.SaveChanges();
            return RedirectToAction(nameof(CinemaList));
        }

        public async Task<IActionResult> DeleteCinema(int id)
        {
            //var cat = db.cinema.FirstOrDefault(c => c.Id == id);
            var cat = await cinemaRepo.GetOneAsync(c => c.Id == id);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\CinemaImg", cat.Img);
            cinemaRepo.Delete(cat);
            //db.cinema.Remove(cat);
            //db.SaveChanges();
            await cinemaRepo.CommitAsync();
            if (System.IO.File.Exists(path)) 
                System.IO.File.Delete(path);
            return RedirectToAction(nameof(CinemaList));
        }
    }
}
