using BuildingBlock.Application.Entities;
using BuildingBlock.Application.Services;
using BuildingBlock.Domain;
using BuildingBlock.Infra.DataBase.EntityFramework.Entities;
using BuildingBlock.Infra.Domain.ValueObjects.EFCore.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlock.Infra.DataBase.EntityFramework
{
    public class AppDbContext : DbContext, IDbContext, IUnitOfWork
    {
        private readonly IDomainEventService? _domainEventService;
        public DbSet<OutboxMessageEntity> OutboxMessageEntities { get; set; }


        public AppDbContext(IDomainEventService? domainEventService, DbContextOptions options) : base(options)
        {
            _domainEventService = domainEventService;

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

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder
                .AddApplicationDomainDataEFCoreConvert();
        }

        #region Domain Events
        public override int SaveChanges()
        {
            var models = GetAggregateRoots();
            var events = _domainEventService?.GetDamainOutboxEvents(models) ?? [];
            this.AddRange(events);

            var changes = base.SaveChanges();

            _domainEventService?.EmitEvents(events);
            base.SaveChanges();

            return changes;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var models = GetAggregateRoots();
            var events = _domainEventService?.GetDamainOutboxEvents(models) ?? [];
            this.AddRange(events);

            var changes = await base.SaveChangesAsync(cancellationToken);

            _domainEventService?.EmitEvents(events);
            await base.SaveChangesAsync(CancellationToken.None);

            return changes;
        }

        private List<IAggregateRoot> GetAggregateRoots()
        {
            return ChangeTracker
              .Entries<IAggregateRoot>()
              .Select(x => x.Entity)
              .ToList();
        }
        #endregion
    }
}
