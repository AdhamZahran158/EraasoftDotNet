using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Task13.DataAccess;
using Task13.Models;
using Task13_v2.Repositories;
using Task13_v2.ViewModels;

namespace Task13.Controllers
{
    public class MovieController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        Repository<Movie> movieRepo ;
        Repository<Category> catRepo ;
        Repository<Cinema> cinemaRepo;
        Repository<Actor> actorRepo;
        Repository<MovieSubImage> subImgRepo;

        public MovieController()
        {
            this.movieRepo = new(db);
            this.catRepo = catRepo = new Repository<Category>(db);
            this.cinemaRepo = cinemaRepo = new Repository<Cinema>(db);
            this.actorRepo = actorRepo = new Repository<Actor>(db);
            this.subImgRepo = subImgRepo = new Repository<MovieSubImage>(db);
        }

        public async Task<IActionResult> MovieList()
        {
            var movies = await movieRepo.GetAsync();
            var categories = await catRepo.GetAsync();
            //var movies = db.movies.AsQueryable();
            // var moviesWithCat = db.movies.Join(db.categories,
            //m => m.CategoryId,
            //c => c.Id,
            //(m, c) => new MovieListVM
            //{
            //CategoryName = c.Name,
            //   MovieName = m.Name,
            //   MoviePrice = m.Price,
            //   MovieStatus = m.Status,
            //   MovieDate = m.CreatedDate,
            //   MovieDescription = m.Description,
            //   MovieMainImg = m.MainImg,
            //   MovieId = m.Id
            //});
            var moviesWithCat = await movieRepo.JoinAsync<Category, object, MovieListVM>(catRepo, m => m.CategoryId, c => c.Id, (m, c) => new()
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
        public async Task<IActionResult> CreateMovie()
        {
            //var categories = db.categories.AsQueryable();
            //var cinema = db.cinema.AsQueryable();
            var categories = await catRepo.GetAsync(tracked: false);
            var cinema = await cinemaRepo.GetAsync(tracked: false);
            ViewBag.cinema = cinema;
            ViewBag.actors = await actorRepo.GetAsync(tracked: false);
            return View(categories.AsEnumerable());
        }
        [HttpPost]
        public async Task<IActionResult> CreateMovie(MovieVM movieVM, IFormFile? mainImg, List<IFormFile>? subImgs, List<int> selectedActors)
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
                        stream.Close();
                    }
                    movieVM.MainImg = fileName;
                }
                //var newMovie = db.movies.Add(new()
                //{
                //Name = movieVM.Name,
                //    MainImg = movieVM.MainImg,
                //    Price = movieVM.Price,
                //    CategoryId = movieVM.CatId,
                //    CinemaId = movieVM.CinemaId,
                //    Description = movieVM.Description,
                //    CreatedDate = movieVM.Date,
                //    Status = movieVM.Status
                //});
                //db.SaveChanges();
                var newMovie = await movieRepo.CreateAsync(new()
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
                            stream.Close();
                        }
                        //db.sub_images.Add(new()
                        //{
                        //Img = fileName,
                        //    MovieId = newMovie.Entity.Id,
                        //    Title = $"Movie {newMovie.Entity.Name} Photo"
                        //});
                        await subImgRepo.CreateAsync(new()
                        {
                            Img = fileName,
                            MovieId = newMovie.Id,
                            Title = $"Movie {newMovie.Name} Photo"
                        });
                        //db.SaveChanges();
                        await subImgRepo.CommitAsync();
                    }
                }
                if (selectedActors is not null)
                {
                    //var actors = db.actors.Where(a => selectedActors.Contains(a.Id)).ToList();
                    var actors = (await actorRepo.GetAsync(a => selectedActors.Contains(a.Id))).ToList();
                    newMovie.Actors = actors;
                    //db.SaveChanges();
                    await movieRepo.CommitAsync();
                }
            }

            
            return RedirectToAction(nameof(MovieList));
        }

        [HttpGet]
        public async Task<IActionResult> EditMovie(int id)
        {
            //var movie = db.movies.FirstOrDefault(m => m.Id == id);
            //var categories = db.categories.AsNoTracking().AsEnumerable().ToList();
            //var cinema = db.cinema.AsNoTracking().AsEnumerable().ToList();
            var movie = await movieRepo.GetOneAsync(m => m.Id == id);
            var categories = await catRepo.GetAsync(tracked: false);
            var cinema = await cinemaRepo.GetAsync(tracked: false);
            return View(new EditMovieVM
            {
                Movie = movie,
                Categories = categories.ToList(),
                Cinema = cinema.ToList()
            });
        }
        [HttpPost]
        public async Task<IActionResult> EditMovie(int id,MovieVM movieVM, IFormFile? mainImg, List<IFormFile>? subImgs)
        {
            //var movie = db.movies.FirstOrDefault(m => m.Id ==id);
            //var subImgsInDb = db.sub_images.AsQueryable();
            //var subImgsInSystem = subImgsInDb.Where(s => s.MovieId == movie.Id);
            var movie = await movieRepo.GetOneAsync(m =>m.Id == id);
            var subImgsInSystem = await subImgRepo.GetAsync(s => s.MovieId == id);
            if (movie == null) {
                return NotFound();
            }
            if (mainImg is not null && mainImg.Length> 0)
            {
                var fileName = Guid.NewGuid().ToString()+Path.GetExtension(mainImg.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\MoviesMainImg", fileName);
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\MoviesMainImg", movie.MainImg);

                using(var stream = System.IO.File.Create(filePath))
                {
                    mainImg.CopyTo(stream);
                }

                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
                movie.MainImg = fileName;
                await movieRepo.CommitAsync();
                //db.SaveChanges();
            }
            if(subImgs != null && subImgs.Count > 0)
            {
                foreach (var item in subImgsInSystem)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\MoviesSubImg", item.Img);
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);
                    //db.sub_images.Remove(item);
                    subImgRepo.Delete(item);
                }
                //db.SaveChanges();
                await subImgRepo.CommitAsync();
                foreach (var item in subImgs)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(item.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\MoviesSubImg", fileName);
                    if(!System.IO.File.Exists (filePath))
                    {
                        System.IO.File.Create(filePath);
                    }
                    //db.sub_images.Add(new()
                    //{
                    //Img = fileName,
                    //    MovieId = movie.Id,
                    //    Title = $"Movie {movie.Name} Photo"
                    //});
                    //db.SaveChanges();
                    await subImgRepo.CreateAsync(new()
                    {
                        Img = fileName,
                        MovieId = movie.Id,
                        Title = $"Movie {movie.Name} Photo"
                    });
                    await subImgRepo.CommitAsync();
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
                //db.SaveChanges();
                await movieRepo.CommitAsync();
            }

            return RedirectToAction(nameof(MovieList));
        }

        public async Task<IActionResult> DeleteMovie(int id)
        {
            //var reqMovie = db.movies.FirstOrDefault(x => x.Id == id);
            var reqMovie = await movieRepo.GetOneAsync(x => x.Id == id);
            //var unwantedSubImgs = db.sub_images.Where(s => s.MovieId == id);
            var unwantedSubImgs = await subImgRepo.GetAsync(s => s.MovieId ==  id);
            foreach (var item in unwantedSubImgs)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\MoviesSubImg", item.Img);
                if(System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
                //db.sub_images.Remove(item);
                subImgRepo.Delete(item);
            }
            //db.SaveChanges();
            await subImgRepo.CommitAsync();
            var mainImgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\MoviesMainImg", reqMovie.MainImg);
            if(System.IO.File.Exists(mainImgPath))
                System.IO.File.Delete(mainImgPath);
            //db.movies.Remove(reqMovie);
            movieRepo.Delete(reqMovie);
            //db.SaveChanges();
            await movieRepo.CommitAsync();
            return RedirectToAction(nameof(MovieList));
        }
    }
}
