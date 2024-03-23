using BuildingBlock.Domain.Entities;
using BuildingBlock.Infra.DataBase.MongoDB;
using MongoDB.Driver;

namespace BuildingBlock.Infra.DataBase.Repositories
{
    public class Repository<TEntity>(AppMongoClient appMongoClient) : IRepository<TEntity> where TEntity : Entity
    {
        private readonly IMongoCollection<TEntity> _collection = appMongoClient.GetCollection<TEntity>();

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task RemoveAsync(TEntity entity)
        {
            return _collection.DeleteOneAsync(x => x.Id == entity.Id);
        }

        public Task SaveAsync(TEntity entity)
        {
            return _collection.InsertOneAsync(entity);
        }

        public Task SaveRangeAsync(IReadOnlyCollection<TEntity> entities)
        {
            return _collection.InsertManyAsync(entities);
        }
    }
}
