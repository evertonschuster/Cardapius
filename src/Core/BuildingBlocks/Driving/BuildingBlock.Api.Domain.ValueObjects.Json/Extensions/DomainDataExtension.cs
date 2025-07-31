using BuildingBlock.Api.Domain.ValueObjects.Json.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Api.Domain.ValueObjects.Json.Extensions
{
    public static class DomainDataExtension
    {
        /// <summary>
        /// Registers JSON and MVC options configurations for domain data serialization in the application's service collection.
        /// </summary>
        public static void AddApplicationDomainDataJsonConvert(this IServiceCollection services)
        {
            services.ConfigureOptions<JsonOptionsConfiguration>();
            services.ConfigureOptions<MvcOptionsConfiguration>();
        }
    }
}
