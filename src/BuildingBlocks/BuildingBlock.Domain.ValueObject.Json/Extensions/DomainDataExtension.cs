using BuildingBlock.Domain.ValueObject.Json.Configurations;

namespace BuildingBlock.Domain.ValueObject.Json.Extensions
{
    public static class DomainDataExtension
    {
        public static void AddApplicationDomainData(this IServiceCollection services)
        {
            services.ConfigureOptions<JsonOptionsConfiguration>();
            services.ConfigureOptions<MvcOptionsFormattersConfigure>();
        }
    }
}
