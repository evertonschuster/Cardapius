using BuildingBlock.Domain.ValueObjects;
using BuildingBlock.Domain.ValueObjects.Phones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BuildingBlock.Api.Domain.ValueObjects.Json.Configurations
{
    internal class JsonOptionsConfiguration : IConfigureOptions<MvcNewtonsoftJsonOptions>
    {
        public JsonOptionsConfiguration(IEnumerable<JsonConverter> jsonConverters)
        {
            JsonConverters = jsonConverters ?? throw new ArgumentNullException(nameof(jsonConverters));
        }

        public IEnumerable<JsonConverter> JsonConverters { get; set; }

        public void Configure(MvcNewtonsoftJsonOptions options)
        {
            options.UseCamelCasing(processDictionaryKeys: true);
            ValueObjectConverterRegistrar.RegisterAll(options.SerializerSettings, [typeof(IValueObject).Assembly]);
        }
    }
}
