using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9_Pt._2.P02_SalesDatabase.Models;

namespace Task9_Pt._2.P02_SalesDatabase.Data
{
    internal class SalesContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Sale> Sales { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Sales_Database;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasKey(nameof(Product.ProductId));
            modelBuilder.Entity<Product>().Property(nameof(Product.Name)).HasMaxLength(50).IsUnicode(true);

            modelBuilder.Entity<Customer>().HasKey(nameof(Customer.CustomerId));
            modelBuilder.Entity<Customer>().Property(n => n.Name).HasMaxLength(100).IsUnicode(true);
            modelBuilder.Entity<Customer>().Property(e => e.Email).HasMaxLength(80).IsUnicode(false);

            modelBuilder.Entity<Store>().HasKey(nameof(Store.StoreId));
            modelBuilder.Entity<Store>().Property(n => n.Name).HasMaxLength(80).IsUnicode(true);

            modelBuilder.Entity<Sale>().HasKey(nameof(Sale.SaleId));
            modelBuilder.Entity<Sale>().HasOne(s => s.Product).WithMany(p => p.Sales).HasForeignKey(k => k.ProductId);
            modelBuilder.Entity<Sale>().HasOne(s=> s.Customer).WithMany(c => c.Sales).HasForeignKey(k => k.CustomerId);
            modelBuilder.Entity<Sale>().HasOne(s => s.Store).WithMany(s => s.Sales).HasForeignKey(k => k.StoreId);
        }
    }
}
