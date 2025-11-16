using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task13.DataAccess;
using Task13_v2.ViewModels;

namespace Task13.Controllers
{
    public class MovieController : Controller
    {
        ApplicationDbContext db = new(); 

        public IActionResult MovieList()
        {
            var movies = db.movies.AsQueryable();
            var moviesWithCat = movies.Join(db.categories,m => m.CategoryId, c=> c.Id,(m,c)=> new
            {
               categoryName=c.Name,
               movieName = m.Name,
               moviePrice = m.Price,
               movieStatus = m.Status,
               movieDate = m.CreatedDate,
               movieDescription = m.Description,
               movieMainImg = m.MainImg
               
            });
            return View(moviesWithCat.AsEnumerable());
        }
        [HttpGet]
        public IActionResult CreateMovie()
        {
            var categories = db.categories.AsQueryable();
            var cinema = db.cinema.AsQueryable();
            ViewBag.cinema = cinema.AsEnumerable();
            return View(categories.AsEnumerable());
        }
        [HttpPost]
        public IActionResult CreateMovie(MovieVM movieVM, IFormFile mainImg, List<IFormFile> subImgs)
        {
            if (movieVM != null)
            {
                if (mainImg != null && mainImg.Length>0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(mainImg.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\MoviesMainImg",fileName);
                    if (!System.IO.File.Exists(filePath))
                    {
                        var stream = System.IO.File.Create(filePath);
                        mainImg.CopyTo(stream);
                    }
                    movieVM.MainImg = fileName;
                }
                var newMovie = db.movies.Add(new()
                {
                    Name = movieVM.Name,
                    MainImg = movieVM.MainImg,
                    Price = movieVM.Price,
                    CategoryId = movieVM.CatId,
                    CinemaId = movieVM.CinemaId,
                    Description = movieVM.Description,
                    CreatedDate = movieVM.Date,
                    Status = movieVM.Status
                });
                db.SaveChanges();
                if (subImgs != null && subImgs.Count > 0)
                {
                    foreach (var item in subImgs)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(item.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Images\\MoviesSubImg",fileName);
                        if (!System.IO.File.Exists(filePath))
                        {
                            var stream = System.IO.File.Create(filePath);    
                            item.CopyTo(stream);
                        }
                        db.sub_images.Add(new()
                        {
                            Img = fileName,
                            MovieId = newMovie.Entity.Id,
                            Title = $"Movie {newMovie.Entity.Name} Photo"
                        });
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction(nameof(MovieList));
        }
    }
}
