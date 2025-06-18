using BuildingBlock.Domain.ValueObjects;
using Newtonsoft.Json;

namespace BuildingBlock.Api.Domain.ValueObjects.Json
{
    public class ValueObjectJsonConverter<TValueObject, TValue, TType> : JsonConverter<TType>
         where TValueObject : IValueObject<TValue, TType>
    {
        public override TType? ReadJson(JsonReader reader, Type objectType, TType? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var rawValue = reader.Value?.ToString();
            if (rawValue is TValue aa)
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

            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, TType? value, JsonSerializer serializer)
        {
            var rawValue = value?.ToString();
            writer.WriteValue(rawValue);
        }
    }
}