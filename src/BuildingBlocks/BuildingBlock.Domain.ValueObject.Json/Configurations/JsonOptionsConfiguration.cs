using BuildingBlock.Domain.ValueObject.Json.Emails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BuildingBlock.Domain.ValueObject.Json.Configurations
{
    public class JsonOptionsConfiguration : IConfigureOptions<JsonOptions>
    {
        public void Configure(JsonOptions options)
        {
            //TODO: Auto registrarion with ioc
            options.JsonSerializerOptions.Converters.Add(new EmailJsonConverter());
        }
    }
}
