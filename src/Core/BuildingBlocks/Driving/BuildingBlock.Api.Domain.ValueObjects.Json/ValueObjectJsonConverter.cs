using BuildingBlock.Domain.ValueObjects;
using Newtonsoft.Json;
using System.ComponentModel;

namespace BuildingBlock.Api.Domain.ValueObjects.Json
{
    public class ValueObjectJsonConverter<TValueObject, TValue, TType> : JsonConverter<TType>
         where TValueObject : IValueObject<TValue, TType>
    {
        public override TType? ReadJson(JsonReader reader, Type objectType, TType? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var rawString = reader.Value?.ToString();

            var converter = TypeDescriptor.GetConverter(typeof(TValue));
            if (!converter.CanConvertFrom(typeof(string)))
                throw new JsonSerializationException($"Cannot convert from string to {typeof(TValue).Name}");

            var typedValue = converter.ConvertFromInvariantString(rawString);
            if (typedValue is TValue aa)
            {
                var value = TValueObject.Parse(aa);
                if (value.IsSuccess)
                {
                    return value.Value;
                }
                else
                {
                    throw new JsonSerializationException(string.Join(", ", value.Errors));
                }
            }

            //TODO: Handle null or invalid input
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, TType? value, JsonSerializer serializer)
        {
            var rawValue = value?.ToString();
            writer.WriteValue(rawValue);
        }
    }
}