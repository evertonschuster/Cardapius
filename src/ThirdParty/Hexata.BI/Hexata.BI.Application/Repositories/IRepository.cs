using Hexata.BI.Application.Entities;

namespace Hexata.BI.Application.Repositories
{
    ///https://medium.com/@marekzyla95/mongo-repository-pattern-700986454a0e
    public interface IRepository<TDocument, TId> where TDocument : IEntity<TId>
    {
        IQueryable<TDocument> AsQueryable();

        TDocument FindById(TId id);

        Task<TDocument> FindByIdAsync(TId id);

        void InsertOne(TDocument document);

        Task InsertOneAsync(TDocument document);

        void ReplaceOne(TDocument document);

        Task ReplaceOneAsync(TDocument document);

        TDocument UpsertOne(TDocument document);

        Task<TDocument> UpsertOneAsync(TDocument document);

        void UpsertMultiple(ICollection<TDocument> documents);

        Task UpsertMultipleAsync(ICollection<TDocument> documents);

        void DeleteById(TId id);

        Task DeleteByIdAsync(TId id);
    }

    public interface IRepository<TDocument>: IRepository<TDocument, Guid> where TDocument : IEntity<Guid>
    {

    }
}
