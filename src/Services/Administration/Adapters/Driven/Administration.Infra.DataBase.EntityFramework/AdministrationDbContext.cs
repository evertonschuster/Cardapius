using Administration.Domain.Restaurants.Models;
using BuildingBlock.Domain;
using BuildingBlock.Infra.Domain.ValueObjects.EFCore.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Administration.Infra.DataBase.EntityFramework
{
    public class AdministrationDbContext : AppDbContext, IUnitOfWork
    {
        public AdministrationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Restaurant> Restaurants { get; private set; }

        public int Commit()
        {
            return this.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return this.SaveChangesAsync();
        }

        public void Rollback()
        {
            this.Database.RollbackTransaction();
        }

        public Task RollbackAsync()
        {
            return this.Database.RollbackTransactionAsync();
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .AddApplicationDomainDataEFCoreConvert();
        }
    }
}