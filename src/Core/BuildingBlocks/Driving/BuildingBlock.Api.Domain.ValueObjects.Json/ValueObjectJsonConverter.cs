using BuildingBlock.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BuildingBlock.Api.Domain.ValueObjects.Json
{
    public class ValueObjectJsonConverter<TValueObject, TValue, TType> : JsonConverter<TValueObject>
     where TValueObject : IValueObject<TValue, TType>
    {
        private static readonly Func<TValue?, Result<TType>> _parseDelegate = CreateParseDelegate();

        private static readonly JsonConverter<TValue> _valueConverter = (JsonConverter<TValue>)(
            JsonSerializerOptions.Default.GetConverter(typeof(TValue))
            ?? throw new InvalidOperationException($"No converter for type {typeof(TValue)}."));

        private static Func<TValue?, Result<TType>> CreateParseDelegate()
        {
            var method = typeof(TValueObject).GetMethod("Parse", BindingFlags.Public | BindingFlags.Static);
            if (method == null)
                throw new InvalidOperationException($"Método Parse não encontrado em {typeof(TValueObject)}");

            var valueParam = Expression.Parameter(typeof(TValue), "s");
            var call = Expression.Call(method, valueParam);
            return Expression.Lambda<Func<TValue?, Result<TType>>>(call, valueParam).Compile();
        }

        public override TValueObject? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            TValue value = _valueConverter.Read(ref reader, typeof(TValue), options)!;
            var result = _parseDelegate(value);

            if (result.IsSuccess)
            {
                return (TValueObject)(object)result.Value!;
            }

            throw new JsonException($"Erro ao desserializar {typeof(TValueObject)}: {result.Errors}");
        }

        public override void Write(Utf8JsonWriter writer, TValueObject value, JsonSerializerOptions options)
        {
            var str = value?.ToString();
            writer.WriteStringValue(str);
        }
    }

}
