using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Task13.DataAccess;
using Task13.Models;
using Task13_v2.Repositories;
using Task13_v2.Repositories.IRepositories;

namespace Task13_v2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ActorController : Controller
    {
        //ApplicationDbContext db = new();
        IRepository<Actor> actorRepo;
        public ActorController(IRepository<Actor> actorRepo)
        {
            this.actorRepo = actorRepo;
        }
        public async Task<IActionResult> ActorList()
        {
            //var actors = db.actors.AsQueryable();
            var actors = await actorRepo.GetAsync(tracked: false);
            return View(actors);
        }

        [HttpGet]
        public async Task<IActionResult> AddActor()
        {
            //var actors = db.actors.AsNoTracking().AsEnumerable();
            var actors = await actorRepo.GetAsync(tracked:false);
            return View(actors);
        }

        [HttpPost]
        public async Task<IActionResult> AddActor(Actor actor, IFormFile? Img)
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
            await actorRepo.CreateAsync(new Actor
            {
                Name = actor.Name,
                Img = imgName
            });
            await actorRepo.CommitAsync();
            return RedirectToAction(nameof(ActorList));
        }

        [HttpGet]
        public async Task<IActionResult> EditActor(int id)
        {
            //var actor = db.actors.FirstOrDefault(a => a.Id == id);
            var actor = await actorRepo.GetOneAsync(a => a.Id == id);
            return View(actor);
        }

        [HttpPost]
        public async Task<IActionResult> EditActor(Actor actor, IFormFile? Img, int id)
        {
            //var specActor = db.actors.FirstOrDefault(a => a.Id == id);
            var specActor = await actorRepo.GetOneAsync( a => a.Id == id);
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
            //db.SaveChanges();
            await actorRepo.CommitAsync();

            return RedirectToAction(nameof (ActorList));
        }

        public async Task<IActionResult> DeleteActor(int id)
        {
            //var delActor = db.actors.FirstOrDefault(a => a.Id == id);
            var delActor = await actorRepo.GetOneAsync(a => a.Id == id);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\ActorsImg", delActor.Img);
            if(System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
            //db.actors.Remove(delActor);
            //db.SaveChanges();
            actorRepo.Delete(delActor);
            await actorRepo.CommitAsync();

            return RedirectToAction(nameof(ActorList));
        }
    }
}
