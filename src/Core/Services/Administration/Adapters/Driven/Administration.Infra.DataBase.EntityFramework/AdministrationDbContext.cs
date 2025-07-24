using Administration.Domain.Products.Entities;
using Administration.Domain.Restaurants.Models;
using BuildingBlock.Application.Services;
using Microsoft.EntityFrameworkCore;

namespace Administration.Infra.DataBase.EntityFramework
{
    public class AdministrationDbContext(IDomainEventService? domainEventService, DbContextOptions options) : AppDbContext(domainEventService, options)
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
