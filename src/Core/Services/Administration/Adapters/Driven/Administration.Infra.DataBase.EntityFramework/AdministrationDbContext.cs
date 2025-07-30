using Administration.Domain.Products.Entities;
using Administration.Domain.Restaurants.Models;
using BuildingBlock.Infra.DataBase.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Administration.Infra.DataBase.EntityFramework
{
    public class AdministrationDbContext(DbContextContainer contextContainer, DbContextOptions options) : AppDbContext(contextContainer, options)
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("Administration");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AdministrationDbContext).Assembly);

            modelBuilder.AddSoftDeleteAll();
        }
    }
}
