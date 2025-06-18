using BuildingBlock.Domain.ValueObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlock.Api.Domain.ValueObjects.Json
{
    public static class ValueObjectConverterRegistrar
    {
        public static void RegisterAll(JsonSerializerSettings settings, Assembly[] assemblies)
        {
            var valueObjectInterface = typeof(IValueObject<,>);
            var genericConverterType = typeof(ValueObjectJsonConverter<,,>);

            var types = assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => !t.IsAbstract && !t.IsInterface)
                .Select(t => new
                {
                    Type = t,
                    Interface = t.GetInterfaces()
                        .FirstOrDefault(i =>
                            i.IsGenericType &&
                            i.GetGenericTypeDefinition() == valueObjectInterface)
                })
                .Where(x => x.Interface != null)
                .ToList();

            foreach (var x in types)
            {
                var tValueObject = x.Type;
                var typeArgs = x.Interface!.GetGenericArguments();
                var tValue = typeArgs[0];
                var tType = typeArgs[1];

                var converterGenericType = genericConverterType.MakeGenericType(tValueObject, tValue, tType);
                var converter = Activator.CreateInstance(converterGenericType) as JsonConverter;

                if (converter != null)
                {
                    settings.Converters.Add(converter);
                }
            }
        }
    }
}
