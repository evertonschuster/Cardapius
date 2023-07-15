using Microsoft.EntityFrameworkCore;

namespace BuildingBlock.Infra.DataBase.EntityFramework
{
    public class AppDbContext : DbContext, IDbContext
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

        public void Insert<TEntity>(TEntity entity) where TEntity : Entity
        {
            this.Set<TEntity>()
                .Add(entity);
        }

        public void InsertRange<TEntity>(IReadOnlyCollection<TEntity> entities) where TEntity : Entity
        {
            this.Set<TEntity>()
               .AddRange(entities);
        }

        public new void Remove<TEntity>(TEntity entity)
            where TEntity : Entity
        {
            this.Set<TEntity>()
                .Remove(entity);
        }
    }
}
