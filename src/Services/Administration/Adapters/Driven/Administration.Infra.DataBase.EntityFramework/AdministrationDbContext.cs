using Administration.Domain.Restaurants.Models;
using BuildingBlock.Infra.Domain.ValueObjects.EFCore.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Administration.Infra.DataBase.EntityFramework
{
    public class AdministrationDbContext : AppDbContext
    {
        public AdministrationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Restaurant> Restaurants { get; private set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .AddApplicationDomainDataEFCoreConvert();
        }
    }
}