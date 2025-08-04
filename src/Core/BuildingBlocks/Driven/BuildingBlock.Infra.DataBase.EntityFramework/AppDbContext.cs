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


        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class with optional dependency injection support and disables automatic query tracking.
        /// </summary>
        /// <param name="dbContextContainer">An optional container for injecting EF Core interceptors.</param>
        /// <param name="options">The options to be used by the DbContext.</param>
        public AppDbContext(DbContextContainer? dbContextContainer, DbContextOptions options) : base(options)
        {
            _dbContextContainer = dbContextContainer;

            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        #region CRUD Operations
        /// <summary>
        /// Returns a <see cref="DbSet{TEntity}"/> for the specified entity type.
        /// </summary>
        /// <typeparam name="TEntity">The entity type, which must inherit from <see cref="Entity"/>.</typeparam>
        /// <returns>A <see cref="DbSet{TEntity}"/> instance for querying and saving entities of the specified type.</returns>
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

        /// <summary>
        /// Asynchronously removes the specified entity from the context.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity to remove.</typeparam>
        /// <param name="entity">The entity instance to be removed.</param>
        public Task RemoveAsync<TEntity>(TEntity entity)
            where TEntity : Entity
        {
            return Task.Run(() => this.Set<TEntity>()
                .Remove(entity));
        }

        #endregion


        #region Unit of Work
        /// <summary>
        /// Saves all changes made in the context to the database synchronously.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        public int Commit()
        {
            return this.SaveChanges();
        }

        public Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return this.SaveChangesAsync(cancellationToken);
        }

        public void Rollback()
        {
            this.Database.RollbackTransaction();
        }

        /// <summary>
        /// Asynchronously rolls back the current database transaction, discarding any uncommitted changes.
        /// </summary>
        public Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            return this.Database.RollbackTransactionAsync(cancellationToken);
        }

        #endregion

        /// <summary>
        /// Configures the EF Core model for the application, including domain-specific value conversions and the outbox message entity configuration.
        /// </summary>
        /// <param name="modelBuilder">The builder used to construct the model for the context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddApplicationDomainDataEFCoreConvert();
            modelBuilder.ApplyConfiguration(new OutboxMessageEntityConfiguration());
        }

        /// <summary>
        /// Configures the database context by adding interceptors for auditing, soft deletion, and domain event emission if available.
        /// </summary>
        /// <param name="optionsBuilder">The builder used to configure the context options.</param>
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

        /// <summary>
        /// Configures model conventions by adding application domain-specific EF Core value conversions.
        /// </summary>
        /// <param name="configurationBuilder">The builder used to configure model conventions.</param>
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder
                .AddApplicationDomainDataEFCoreConvert();
        }
    }
}
