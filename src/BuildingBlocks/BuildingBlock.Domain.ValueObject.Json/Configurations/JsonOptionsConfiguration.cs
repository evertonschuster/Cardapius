using BuildingBlock.Domain.ValueObject.Json.Emails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;

namespace BuildingBlock.Domain.ValueObject.Json.Configurations
{
    public class JsonOptionsConfiguration : IConfigureOptions<JsonOptions>
    {
        public JsonOptionsConfiguration(IEnumerable<JsonConverter> jsonConverters)
        {
            JsonConverters = jsonConverters ?? throw new ArgumentNullException(nameof(jsonConverters));
        }

        public IEnumerable<JsonConverter> JsonConverters { get; set; }

        public void Configure(JsonOptions options)
        {
            foreach (var jsonConverter in JsonConverters)
            {
                options.JsonSerializerOptions.Converters.Add(jsonConverter);
            }
        }
    }
}
