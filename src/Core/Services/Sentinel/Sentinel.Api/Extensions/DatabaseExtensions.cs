using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sentinel.Api.Data;

namespace Sentinel.Api.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SentinelDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Default"));
                options.UseOpenIddict();
            });

            return services;
        }
    }
}
