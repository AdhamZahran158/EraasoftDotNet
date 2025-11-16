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
            var moviesWithCat = db.movies.Join(db.categories,
           m => m.CategoryId,
           c => c.Id,
           (m, c) => new MovieListVM
           {
               CategoryName = c.Name,
               MovieName = m.Name,
               MoviePrice = m.Price,
               MovieStatus = m.Status,
               MovieDate = m.CreatedDate,
               MovieDescription = m.Description,
               MovieMainImg = m.MainImg,
               MovieId = m.Id
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
        public IActionResult CreateMovie(MovieVM movieVM, IFormFile? mainImg, List<IFormFile>? subImgs)
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

        [HttpGet]
        public IActionResult EditMovie(int id)
        {
            var movie = db.movies.FirstOrDefault(m => m.Id == id);
            var categories = db.categories.AsNoTracking().AsEnumerable().ToList();
            var cinema = db.cinema.AsNoTracking().AsEnumerable().ToList();
            return View(new EditMovieVM
            {
                Movie = movie,
                Categories = categories,
                Cinema = cinema
            });
        }
        [HttpPost]
        public IActionResult EditMovie(int id,MovieVM movieVM, IFormFile? mainImg, List<IFormFile>? subImgs)
        {
            var movie = db.movies.FirstOrDefault(m => m.Id ==id);
            var subImgsInDb = db.sub_images.AsQueryable();
            var subImgsInSystem = subImgsInDb.Where(s => s.MovieId == movie.Id);
            if (movie == null) {
                return NotFound();
            }
            if (mainImg is not null && mainImg.Length> 0)
            {
                var fileName = Guid.NewGuid().ToString()+Path.GetExtension(mainImg.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\MoviesMainImg", fileName);
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\MoviesMainImg", movie.MainImg);

                if(!System.IO.File.Exists(filePath))
                {
                    System.IO.File.Create(filePath);
                    System.IO.File.Delete(oldPath);
                }
                movie.MainImg = fileName;
                db.SaveChanges();
            }
            if(subImgs != null && subImgs.Count > 0)
            {
                foreach (var item in subImgsInSystem)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\MoviesSubImg", item.Img);
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);
                    db.sub_images.Remove(item);
                }
                db.SaveChanges();
                foreach (var item in subImgs)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(item.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\MoviesSubImg", fileName);
                    if(!System.IO.File.Exists (filePath))
                    {
                        System.IO.File.Create(filePath);
                    }
                    db.sub_images.Add(new()
                    {
                        Img = fileName,
                        MovieId = movie.Id,
                        Title = $"Movie {movie.Name} Photo"
                    });
                    db.SaveChanges();
                }
            }
            if(movieVM is not null)
            {
                movie.Name = movieVM.Name;
                movie.Description= movieVM.Description is null || movieVM.Description == "" ? movie.Description: movieVM.Description;
                movie.CategoryId = movieVM.CatId;
                movie.CinemaId = movieVM.CinemaId;
                movie.Price = movieVM.Price;
                movie.Status = movieVM.Status;
                movie.CreatedDate = movieVM.Date;
                db.SaveChanges();
            }

            return RedirectToAction(nameof(MovieList));
        }
    }
}
