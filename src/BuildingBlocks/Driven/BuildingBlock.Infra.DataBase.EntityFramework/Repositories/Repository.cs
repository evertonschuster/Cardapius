namespace BuildingBlock.Infra.DataBase.EntityFramework.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly IDbContext _IDbContext;

        public Repository(IDbContext iDbContext)
        {
            _IDbContext = iDbContext;
        }

        public virtual Task<TEntity?> GetBydIdAsync(Guid id)
        {
            return this._IDbContext.GetBydIdAsync<TEntity>(id);
        }

        public virtual void Save(TEntity entity)
        {
            this._IDbContext.Insert(entity);
        }

        public virtual void SaveRange(IReadOnlyCollection<TEntity> entities)
        {
            this._IDbContext.InsertRange(entities);
        }

        public virtual void Remove(TEntity entity)
        {
            this._IDbContext.Remove(entity);
        }
    }
}
