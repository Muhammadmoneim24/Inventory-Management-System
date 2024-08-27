using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> categories { get; set; }

        public DbSet<Product> products { get; set; }

        public DbSet<Transaction> transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-MN6ULTF\\SSEXP;Database=IMSProj;Trusted_Connection=True;TrustServerCertificate=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Categorey
            modelBuilder.Entity<Category>().HasKey(e=>e.CategoryId);
            modelBuilder.Entity<Category>().Property(e => e.CategoryId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Category>().Property(e=>e.Name).HasMaxLength(100);
            modelBuilder.Entity<Category>().Property(e => e.Description).HasMaxLength(500);
            modelBuilder.Entity<Category>()
               .HasMany(c => c.Products)
               .WithOne(p => p.Category)
               .HasForeignKey(p => p.CategoryId);


            //Product
            modelBuilder.Entity<Product>().HasKey(p => p.ProductId);
            modelBuilder.Entity<Product>().Property(e => e.ProductId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Product>().Property(e => e.Name).HasMaxLength(100);
            modelBuilder.Entity<Product>().Property(e => e.Description).HasMaxLength(500);
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Transactions)
                .WithOne(t => t.Product)
                .HasForeignKey(t => t.ProductId);

            modelBuilder.Entity<Transaction>().HasKey(p => p.TransactionId);
            modelBuilder.Entity<Transaction>().Property(e => e.TransactionId).ValueGeneratedOnAdd();


            base.OnModelCreating(modelBuilder);

        }


    }
}
