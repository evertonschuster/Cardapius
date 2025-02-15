using Administration.Domain.Restaurants.Models;
using BuildingBlock.Domain;
using Microsoft.EntityFrameworkCore;

namespace Administration.Infra.DataBase.EntityFramework
{
    public class AdministrationDbContext(DbContextOptions options) : AppDbContext(options), IUnitOfWork
    {
        public DbSet<Restaurant> Restaurants { get; private set; }

    }
}
