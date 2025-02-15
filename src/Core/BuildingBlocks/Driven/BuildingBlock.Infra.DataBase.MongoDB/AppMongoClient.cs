using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlock.Infra.DataBase.MongoDB
{
    public class AppMongoClient
    {
        private readonly IMongoDatabase mongoDatabase;

        public AppMongoClient(IOptions<DatabaseSettings> storeDatabaseSettings)
        {
            var mongoClient = new MongoClient(storeDatabaseSettings.Value.ConnectionString);
            mongoDatabase = mongoClient.GetDatabase(storeDatabaseSettings.Value.DatabaseName);
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            var collectionName = GetCollectionName<TEntity>();
            return mongoDatabase.GetCollection<TEntity>(collectionName);
        }

        private static string GetCollectionName<TEntity>()
        {
            return typeof(TEntity).Name;
        }
    }
}
