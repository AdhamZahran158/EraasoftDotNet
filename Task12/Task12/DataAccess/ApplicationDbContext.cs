using Microsoft.EntityFrameworkCore;
using Task12.Models;

namespace Task12.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> users {  get; set; }
        public DbSet<Todo> todos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=TodoList;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Todo>().HasKey(t => t.Id);
            modelBuilder.Entity<Todo>().HasOne(t => t.User);
        }
    }
}
