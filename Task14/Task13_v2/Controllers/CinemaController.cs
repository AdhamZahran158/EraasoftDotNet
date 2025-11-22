using Microsoft.AspNetCore.Mvc;
using Task13.DataAccess;

namespace Task13.Controllers
{
    public class CinemaController : Controller
    {
        ApplicationDbContext db = new();
        public IActionResult CinemaList()
        {
            var cinemas = db.cinema.AsEnumerable();
            return View(cinemas);
        }
        [HttpGet]
        public IActionResult AddCinema()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCinema(string? Name, IFormFile? Img)
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
                db.cinema.Add(new()
                {
                    Name = Name,
                    Img = fileName
                });
                db.SaveChanges();
            }
            return RedirectToAction(nameof(CinemaList));
        }

        [HttpGet]
        public IActionResult EditCinema(int id)
        {
            var cat = db.cinema.FirstOrDefault(c => c.Id == id);
            return View(cat);
        }
        [HttpPost]
        public IActionResult EditCinema(int id, string Name, IFormFile Img)
        {
            var cat = db.cinema.FirstOrDefault(c => c.Id == id);
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
            db.SaveChanges();
            return RedirectToAction(nameof(CinemaList));
        }

        public IActionResult DeleteCinema(int id)
        {
            var cat = db.cinema.FirstOrDefault(c => c.Id == id);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\CinemaImg", cat.Img);
            db.cinema.Remove(cat);
            db.SaveChanges();
            if (System.IO.File.Exists(path)) 
                System.IO.File.Delete(path);
            return RedirectToAction(nameof(CinemaList));
        }
    }
}
