using BuildingBlock.Domain.ValueObjects;
using Newtonsoft.Json;
using System.ComponentModel;

namespace BuildingBlock.Api.Domain.ValueObjects.Json
{
    public class ValueObjectJsonConverter<TValueObject, TValue, TType> : JsonConverter<TType>
         where TValueObject : IValueObject<TValue, TType>
    {
        private static readonly TypeConverter _converter = TypeDescriptor.GetConverter(typeof(TValue));
        private static readonly Func<TValue?, Result<TType>> _parseFunc = TValueObject.Parse;

        public override TType? ReadJson(
            JsonReader reader,
            Type objectType,
            TType? existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            var rawString = reader.Value?.ToString();

            TValue? typedValue = rawString is null ? default : (TValue)_converter.ConvertFromInvariantString(rawString)!;

            var result = _parseFunc(typedValue);
            if (result.IsSuccess)
                return result.Value;

            throw new JsonSerializationException(
                $"Erro ao desserializar '{rawString}' como {typeof(TValueObject).Name}: "
                + string.Join("; ", result.Errors));
        }

        public override void WriteJson(JsonWriter writer, TType? value, JsonSerializer serializer)
        {
            var rawValue = value?.ToString();
            writer.WriteValue(rawValue);
        }
    }
}