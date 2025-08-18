using Microsoft.EntityFrameworkCore;
using Sentinel.Api.Data;

namespace Sentinel.Api.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SentinelDbContext>(options =>
            {
                var cs = configuration.GetConnectionString("Default");
                if (string.IsNullOrWhiteSpace(cs))
                    throw new InvalidOperationException("Missing connection string 'Default'.");

                options.UseNpgsql(cs, npgsql =>
                {
                    npgsql.EnableRetryOnFailure();
                    npgsql.MigrationsHistoryTable("__EFMigrationsHistory", "Sentinel");
                });
                options.UseOpenIddict();
            });

            return services;
        }
    }
}
