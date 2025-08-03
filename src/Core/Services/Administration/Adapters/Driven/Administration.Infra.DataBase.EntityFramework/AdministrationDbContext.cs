using Administration.Domain.Products.Entities;
using Administration.Domain.Restaurants.Models;
using Administration.Domain.Suppliers.Entities;
using BuildingBlock.Infra.DataBase.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Administration.Infra.DataBase.EntityFramework
{
    public class AdministrationDbContext(DbContextContainer contextContainer, DbContextOptions options) : AppDbContext(contextContainer, options)
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        /// <summary>
        /// Configures the EF Core model for the Administration domain, setting the default schema, applying entity configurations from the current assembly, and enabling soft delete for all entities.
        /// </summary>
        /// <param name="modelBuilder">The builder used to construct the model for the context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("Administration");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AdministrationDbContext).Assembly);

            modelBuilder.AddSoftDeleteAll();
        }
    }
}
