using Administration.Application.Products;
using Administration.Domain.Restaurants.Models;
using Microsoft.EntityFrameworkCore;

namespace Administration.Infra.DataBase.EntityFramework
{
    public class AdministrationDbContext(DbContextOptions options) : AppDbContext(options)
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("Administration");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AdministrationDbContext).Assembly);
        }
    }
}
