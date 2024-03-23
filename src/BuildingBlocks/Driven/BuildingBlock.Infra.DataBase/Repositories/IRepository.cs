namespace BuildingBlock.Infra.DataBase.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity?> GetByIdAsync(Guid id);

        Task SaveAsync(TEntity entity);

        Task SaveRangeAsync(IReadOnlyCollection<TEntity> entities);

        Task RemoveAsync(TEntity entity);
    }
}
