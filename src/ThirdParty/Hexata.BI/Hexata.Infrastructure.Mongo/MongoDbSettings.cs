namespace Hexata.Infrastructure.Mongo
{
    public class MongoDbSettings
    {
        public required string DatabaseName { get; set; }
        public required string ConnectionLocalizationString { get; set; }
    }
}
