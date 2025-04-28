using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace Hexata.Infrastructure.Mongo
{
    internal class Repository<TDocument>
    {
        protected readonly IMongoDatabase _database;
        protected readonly IMongoCollection<TDocument> _collection;

        public Repository(IOptions<MongoDbSettings> optionSettings, string collectionName)
        {
            var settings = optionSettings.Value;

            var mongoSettings = MongoClientSettings.FromConnectionString(settings.ConnectionString);
            mongoSettings.ServerApi = new ServerApi(ServerApiVersion.V1);

            var client = new MongoClient(mongoSettings);

            _database = client.GetDatabase(settings.DatabaseName);
            _collection = _database.GetCollection<TDocument>(collectionName);
        }
    }
}
