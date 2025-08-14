using Sentinel.Api.Data.Seeds;
using Sentinel.Api.Services;

namespace Sentinel.Api.Extensions
{
    public static class InjectServicesExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ISeedService, ClientSeedService>();
            services.AddScoped<ISeedService, RoleSeedService>();
            services.AddScoped<ISeedService, UserSeedService>();
            services.AddSingleton<PasswordGeneratorService>();

            return services;
        }
    }
}
