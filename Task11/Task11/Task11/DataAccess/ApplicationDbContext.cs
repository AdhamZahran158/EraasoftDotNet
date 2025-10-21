using ContactDoctor.Models;
using Microsoft.EntityFrameworkCore;

namespace Task11.Models.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Doctor> doctors {  get; set; }
        public DbSet<Appointment> appointments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ClinicTask11;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Doctor>().HasKey(d => d.Id);

            modelBuilder.Entity<Appointment>().HasKey(d => d.Id);
            modelBuilder.Entity<Appointment>().HasOne(a => a.Doctor).WithMany(d => d.Appointments);
        }
    }
}
