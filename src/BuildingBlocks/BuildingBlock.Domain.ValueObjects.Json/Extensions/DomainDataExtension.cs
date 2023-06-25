using BuildingBlock.Domain.ValueObjects.Json.Configurations;
using BuildingBlock.Domain.ValueObjects.Json.Emails;
using BuildingBlock.Domain.ValueObjects.Json.PersonNames;
using BuildingBlock.Domain.ValueObjects.Json.Phones;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace BuildingBlock.Domain.ValueObjects.Json.Extensions
{
    public static class DomainDataExtension
    {
        public static void AddApplicationDomainDataJsonConvert(this IServiceCollection services)
        {
            services.ConfigureOptions<JsonOptionsConfiguration>();
            services.ConfigureOptions<MvcOptionsFormattersConfigure>();

            services.AddTransient<JsonConverter, EmailJsonConverter>();
            services.AddTransient<JsonConverter, PersonNameConverter>();
            services.AddTransient<JsonConverter, PhoneConverter>();
        }
    }
}
