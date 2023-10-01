using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Api.Version.Extensions
{
    public static class VersionExtension
    {
        public static IServiceCollection AddApplicationVersion(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            })
            .AddMvc()
            .AddApiExplorer(options => options.GroupNameFormat = "'v'VVV");

            return services;
        }
    }
}
