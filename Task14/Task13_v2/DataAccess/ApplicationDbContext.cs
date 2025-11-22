using Microsoft.EntityFrameworkCore;
using Task13.Models;

namespace Task13.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Movie> movies {  get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Cinema> cinema { get; set; }
        public DbSet<MovieSubImage> sub_images { get; set; }
        public DbSet<Actor> actors { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Task13_Cinema;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>().HasKey(m => m.Id);
            modelBuilder.Entity<Movie>().HasMany(m => m.MovieSubImages).WithOne(m => m.Movie);
            modelBuilder.Entity<Movie>().HasMany(m => m.Actors).WithMany(a => a.Movies);
            modelBuilder.Entity<Movie>().HasOne(m => m.Category);
            modelBuilder.Entity<Movie>().HasOne(m => m.Cinema);

            modelBuilder.Entity<Category>().HasKey(m => m.Id);

            modelBuilder.Entity<Cinema>().HasKey(m => m.Id);
            modelBuilder.Entity<MovieSubImage>().HasKey(m => m.Id);
            modelBuilder.Entity<Actor>().HasKey(m => m.Id);
        }
    }
}
