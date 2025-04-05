using Hexata.BI.Application.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;


namespace Hexata.Infrastructure.Mongo
{
    public static class MongoExtension
    {
        public static IHostApplicationBuilder AddMongo(this IHostApplicationBuilder builder)
        {
            var configuration = builder.Configuration;

            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

            builder.Services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
            builder.Services.AddSingleton(typeof(IRepository<>), typeof(MongoRepository<>));
            builder.Services.AddSingleton(typeof(IRepository<,>), typeof(MongoRepository<,>));

            return builder;
        }
    }
}
