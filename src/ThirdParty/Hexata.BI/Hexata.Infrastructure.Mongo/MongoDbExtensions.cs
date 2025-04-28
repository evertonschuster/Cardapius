using MongoDB.Bson.Serialization;
using System.ComponentModel;
using System.Reflection;

namespace Hexata.Infrastructure.Mongo
{
    internal static class MongoDbExtensions
    {
        public static void RegisterClassMapWithDisplayName<TClass>(this BsonClassMap<TClass> bsonClass)
        {
            var properties = typeof(TClass).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                var displayNameAttribute = property.GetCustomAttribute<DisplayNameAttribute>();

                if (displayNameAttribute != null)
                {
                    bsonClass.MapMember(property).SetElementName(displayNameAttribute.DisplayName);
                }
                else
                {
                    bsonClass.MapMember(property).SetElementName(property.Name);
                }
            }
        }
    }
}
