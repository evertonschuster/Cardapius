using Hexata.BI.Application.DataBaseSyncs.Sales.Models;
using Hexata.BI.Application.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;


namespace Hexata.Infrastructure.Mongo
{
    public static class MongoExtension
    {
        public static IHostApplicationBuilder AddMongo(this IHostApplicationBuilder builder)
        {
            var configuration = builder.Configuration;

            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Double, new RepresentationConverter(true, true)));

            BsonClassMap.RegisterClassMap<Order>(cm =>
            {
                cm.AutoMap();
                cm.RegisterClassMapWithDisplayName();
                cm.UnmapMember(e => e.Items);
            });

            BsonClassMap.RegisterClassMap<OrderItem>(cm =>
            {
                cm.AutoMap();
                cm.RegisterClassMapWithDisplayName();
            });


            builder.Services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
            builder.Services.AddSingleton<ILocalizationRepository, LocalizationRepository>();
            builder.Services.AddSingleton<IBISaleRepository, BISaleRepository>();

            return builder;
        }
    }
}
