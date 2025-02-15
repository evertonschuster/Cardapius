using BuildingBlock.Domain;
using BuildingBlock.Infra.Domain.ValueObjects.EFCore.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlock.Infra.DataBase.EntityFramework
{
    public class AppDbContext : DbContext, IDbContext, IUnitOfWork
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public new DbSet<TEntity> Set<TEntity>()
            where TEntity : Entity
        {
            return base.Set<TEntity>();
        }

        public Task<TEntity?> GetBydIdAsync<TEntity>(Guid id) where TEntity : Entity
        {
            return this.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task InsertAsync<TEntity>(TEntity entity) where TEntity : Entity
        {
            await this.Set<TEntity>()
                .AddAsync(entity);
        }

        public Task InsertRangeAsync<TEntity>(IReadOnlyCollection<TEntity> entities) where TEntity : Entity
        {
            return this.Set<TEntity>()
                .AddRangeAsync(entities);
        }

        public Task RemoveAsync<TEntity>(TEntity entity)
            where TEntity : Entity
        {
            return Task.Run(() => this.Set<TEntity>()
                .Remove(entity));
        }

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.AddApplicationDomainDataEFCoreConvert();
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder
                .AddApplicationDomainDataEFCoreConvert();
        }
    }
}
