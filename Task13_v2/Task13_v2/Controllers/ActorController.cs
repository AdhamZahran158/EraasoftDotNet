using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task13.DataAccess;
using Task13.Models;

namespace Task13_v2.Controllers
{
    public class ActorController : Controller
    {
        ApplicationDbContext db = new();

        public IActionResult ActorList()
        {
            var actors = db.actors.AsQueryable();
            return View(actors);
        }

        [HttpGet]
        public IActionResult AddActor()
        {
            var actors = db.actors.AsNoTracking().AsEnumerable();
            return View(actors);
        }

        [HttpPost]
        public IActionResult AddActor(Actor actor, IFormFile? Img)
        {
            string imgName = "";
            if (Img != null && Img.Length > 0)
            {
                imgName = Guid.NewGuid().ToString() + Path.GetExtension(Img.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\ActorsImg", imgName);

                using(var stream = System.IO.File.Create(filePath))
                {
                    Img.CopyTo(stream);
                }
            }
            db.actors.Add(new Actor
            {
                Name = actor.Name,
                Img = imgName
            });
            db.SaveChanges();
            return RedirectToAction(nameof(ActorList));
        }

        [HttpGet]
        public IActionResult EditActor(int id)
        {
            var actor = db.actors.FirstOrDefault(a => a.Id == id);
            return View(actor);
        }

        [HttpPost]
        public IActionResult EditActor(Actor actor, IFormFile? Img, int id)
        {
            var specActor = db.actors.FirstOrDefault(a => a.Id == id);
            if(Img is not null && Img .Length > 0)
            {
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\ActorsImg", specActor.Img);
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Img.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\ActorsImg", fileName);

                using(var stream = System.IO.File.Create(filePath))
                {
                    Img.CopyTo(stream);
                }
                if(System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);
                specActor.Img = fileName;

            }
            specActor.Name = actor.Name;
            db.SaveChanges();

            return RedirectToAction(nameof (ActorList));
        }

        public IActionResult DeleteActor(int id)
        {
            var delActor = db.actors.FirstOrDefault(a => a.Id == id);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\ActorsImg", delActor.Img);
            if(System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
            db.actors.Remove(delActor);
            db.SaveChanges();

            return RedirectToAction(nameof(ActorList));
        }
    }
}
