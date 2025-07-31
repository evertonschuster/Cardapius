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

        /// <summary>
        /// Deserializes a JSON value into an instance of the value object type.
        /// </summary>
        /// <param name="reader">The JSON reader positioned at the value to deserialize.</param>
        /// <param name="objectType">The type of the object to create.</param>
        /// <param name="existingValue">The existing value of the object being read, if any.</param>
        /// <param name="hasExistingValue">Indicates whether there is an existing value.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>An instance of the value object type, or null if the input is null.</returns>
        /// <exception cref="JsonSerializationException">
        /// Thrown if the JSON value cannot be converted to the value object type.
        /// </exception>
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

        /// <summary>
        /// Serializes the value object to its string representation and writes it to the JSON output.
        /// </summary>
        public override void WriteJson(JsonWriter writer, TType? value, JsonSerializer serializer)
        {
            var rawValue = value?.ToString();
            writer.WriteValue(rawValue);
        }
    }
}