using BuildingBlock.Application.Entities;
using BuildingBlock.Domain;
using BuildingBlock.Infra.DataBase.EntityFramework.Entities;
using BuildingBlock.Infra.Domain.ValueObjects.EFCore.Extensions;
using Microsoft.EntityFrameworkCore;
namespace BuildingBlock.Infra.DataBase.EntityFramework
{
    public class AppDbContext : DbContext, IDbContext, IUnitOfWork
    {
        private readonly DbContextContainer? _dbContextContainer;
        public DbSet<OutboxMessageEntity> OutboxMessageEntities { get; set; }


        public AppDbContext(DbContextContainer? dbContextContainer, DbContextOptions options) : base(options)
        {
            _dbContextContainer = dbContextContainer;

            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        #region CRUD Operations
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

        #endregion


        #region Unit of Work
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

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddApplicationDomainDataEFCoreConvert();
            modelBuilder.ApplyConfiguration(new OutboxMessageEntityConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_dbContextContainer is not null)
            {
                optionsBuilder.AddInterceptors(
                    _dbContextContainer.AuditingInterceptor,
                    _dbContextContainer.SoftDeleteInterceptor,
                    _dbContextContainer.EmitDomainEventInterceptor
                );
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder
                .AddApplicationDomainDataEFCoreConvert();
        }
    }
}
