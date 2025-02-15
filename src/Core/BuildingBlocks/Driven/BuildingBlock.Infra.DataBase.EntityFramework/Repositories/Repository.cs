namespace BuildingBlock.Infra.DataBase.EntityFramework.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly IDbContext _IDbContext;

        public Repository(IDbContext iDbContext)
        {
            _IDbContext = iDbContext;
        }

        public virtual Task<TEntity?> GetByIdAsync(Guid id)
        {
            return this._IDbContext.GetBydIdAsync<TEntity>(id);
        }

        public virtual Task SaveAsync(TEntity entity)
        {
            return this._IDbContext.InsertAsync(entity);
        }

        public virtual Task SaveRangeAsync(IReadOnlyCollection<TEntity> entities)
        {
            return this._IDbContext.InsertRangeAsync(entities);
        }

        public virtual Task RemoveAsync(TEntity entity)
        {
            return this._IDbContext.RemoveAsync(entity);
        }
    }
}
