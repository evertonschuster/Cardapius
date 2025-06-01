using BuildingBlock.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BuildingBlock.Api.Domain.ValueObjects.Json
{
    public class ValueObjectConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValueObject<,>));
        }

        public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var interfaceType = typeToConvert.GetInterfaces()
                .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValueObject<,>));

            var valueType = interfaceType.GetGenericArguments()[0];
            var converterType = typeof(ValueObjectJsonConverter<,,>)
                .MakeGenericType(typeToConvert, valueType, typeToConvert);

            return (JsonConverter?)Activator.CreateInstance(converterType)!;
        }
    }

}
