using FTN_Skriptarnica_ASPNET.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FTN_Skriptarnica_ASPNET.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            builder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Osnove programiranja",
                    Category = "Knjige",
                    Price = 1200,
                    Description = "Udzbenik za ucenje osnova programiranja u C#",
                    StockQuantity = 15
                },
                new Product
                {
                    Id = 2,
                    Name = "Olovka",
                    Category = "Pribor",
                    Price = 30,
                    Description = "Olovka za pisanje, skolski pribor",
                    StockQuantity = 100
                },
                new Product
                {
                    Id = 3,
                    Name = "Majica",
                    Category = "Garderoba",
                    Price = 900,
                    Description = "Pamucna majica sa logom FTN-a",
                    StockQuantity = 50
                }
                );
        }
    }
}