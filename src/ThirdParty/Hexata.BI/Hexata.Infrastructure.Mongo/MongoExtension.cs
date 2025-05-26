using Hexata.BI.Application.Repositories;
using Hexata.BI.Application.Services.Localizations;
using Hexata.Infrastructure.Mongo.Respositories;
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

            BsonClassMap.RegisterClassMap<AddressDto>(cm =>
            {
                cm.AutoMap();
                cm.RegisterClassMapWithDisplayName();
            });

            builder.Services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
            builder.Services.AddScoped<ILocalizationRepository, LocalizationRepository>();

            return builder;
        }
    }
}
