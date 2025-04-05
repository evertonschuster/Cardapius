using Hexata.BI.Application.Entities;
using Hexata.BI.Application.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Hexata.Infrastructure.Mongo
{
    public class MongoRepository<TDocument, Tid> : IRepository<TDocument, Tid>
    where TDocument : IEntity<Tid>
    {
        private readonly IMongoCollection<TDocument> _collection;

        public MongoRepository(IOptions<MongoDbSettings> optionSettings)
        {
            var settings = optionSettings.Value;

            var mongoSettings = MongoClientSettings.FromConnectionString(settings.ConnectionString);
            mongoSettings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(mongoSettings);

            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        private static protected string GetCollectionName(Type documentType) => documentType.Name;

        public virtual IQueryable<TDocument> AsQueryable()
        {
            return _collection.AsQueryable();
        }

        public virtual TDocument FindById(Tid id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            return _collection.Find(filter).SingleOrDefault();
        }

        public virtual Task<TDocument> FindByIdAsync(Tid id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            return _collection.Find(filter).SingleOrDefaultAsync();
        }


        public virtual void InsertOne(TDocument document)
        {
            _collection.InsertOne(document);
        }

        public virtual Task InsertOneAsync(TDocument document)
        {
            return _collection.InsertOneAsync(document);
        }

        public void InsertMany(ICollection<TDocument> documents)
        {
            _collection.InsertMany(documents);
        }


        public void ReplaceOne(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            _collection.FindOneAndReplace(filter, document);
        }

        public virtual async Task ReplaceOneAsync(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            await _collection.FindOneAndReplaceAsync(filter, document);
        }

        public void DeleteById(Tid id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            _collection.FindOneAndDelete(filter);
        }

        public Task DeleteByIdAsync(Tid id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            return _collection.FindOneAndDeleteAsync(filter);
        }

        public TDocument UpsertOne(TDocument document)
        {
            var options = new FindOneAndReplaceOptions<TDocument>
            {
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After,
            };

            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            return _collection.FindOneAndReplace(filter, document, options);
        }

        public Task<TDocument> UpsertOneAsync(TDocument document)
        {
            var options = new FindOneAndReplaceOptions<TDocument>
            {
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After,
            };

            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            return _collection.FindOneAndReplaceAsync(filter, document, options);
        }

        public void UpsertMultiple(ICollection<TDocument> documents)
        {
            var requests = new List<WriteModel<TDocument>>();
            foreach (var document in documents)
            {
                var filter = new FilterDefinitionBuilder<TDocument>().Eq(doc => doc.Id, document.Id);
                var update = document;
                var request = new ReplaceOneModel<TDocument>(filter, update)
                {
                    IsUpsert = true
                };
                requests.Add(request);
            }
            _collection.BulkWrite(requests);
        }

        public Task UpsertMultipleAsync(ICollection<TDocument> documents)
        {
            var requests = new List<WriteModel<TDocument>>();
            foreach (var document in documents)
            {
                var filter = new FilterDefinitionBuilder<TDocument>().Eq(doc => doc.Id, document.Id);
                var update = document;
                var request = new ReplaceOneModel<TDocument>(filter, update)
                {
                    IsUpsert = true
                };
                requests.Add(request);
            }
            return _collection.BulkWriteAsync(requests);
        }
    }

    public class MongoRepository<TDocument> : MongoRepository<TDocument, Guid>, IRepository<TDocument>
    where TDocument : IEntity<Guid>
    {
        public MongoRepository(IOptions<MongoDbSettings> optionSettings) : base(optionSettings)
        {
        }
    }
}