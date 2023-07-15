namespace BuildingBlock.Infra.DataBase.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity?> GetBydIdAsync(Guid id);

        void Save(TEntity entity);

        void SaveRange(IReadOnlyCollection<TEntity> entities);

        void Remove(TEntity entity);
    }
}
