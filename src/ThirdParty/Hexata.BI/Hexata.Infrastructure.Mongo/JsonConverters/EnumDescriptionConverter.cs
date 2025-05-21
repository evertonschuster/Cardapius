using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hexata.Infrastructure.Mongo.JsonConverters
{
    public class EnumDescriptionSerializer<TEnum> : SerializerBase<TEnum> where TEnum : Enum
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TEnum value)
        {
            var description = GetDescription(value);
            context.Writer.WriteString(description);
        }

        public override TEnum Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var description = context.Reader.ReadString();
            foreach (var field in typeof(TEnum).GetFields())
            {
                var attribute = field.GetCustomAttribute<DescriptionAttribute>();
                if (attribute != null && attribute.Description == description)
                {
                    return (TEnum)field.GetValue(null);
                }

                if (field.Name == description)
                    return (TEnum)field.GetValue(null);
            }

            throw new ArgumentException($"Enum com descrição '{description}' não encontrado.");
        }

        private static string GetDescription(TEnum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attribute = fi.GetCustomAttribute<DescriptionAttribute>();
            return attribute != null ? attribute.Description : value.ToString();
        }
    }
}
