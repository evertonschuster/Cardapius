using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace Hexata.Infrastructure.Mongo.Respositories
{
    internal class Repository<TDocument>
    {
        protected readonly IMongoDatabase _database;
        protected readonly IMongoCollection<TDocument> _collection;

        public Repository(IOptions<MongoDbSettings> optionSettings, string collectionName, Func<MongoDbSettings, string>? getConnectionString = default)
        {
            var settings = optionSettings.Value;
            var connectionString = getConnectionString is not null ? getConnectionString(settings) : settings.ConnectionBiString;

            var mongoSettings = MongoClientSettings.FromConnectionString(connectionString);
            mongoSettings.ServerApi = new ServerApi(ServerApiVersion.V1);

            var client = new MongoClient(mongoSettings);

            _database = client.GetDatabase(settings.DatabaseName);
            _collection = _database.GetCollection<TDocument>(collectionName);
        }
    }
}
