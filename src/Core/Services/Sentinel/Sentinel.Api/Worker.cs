using Microsoft.EntityFrameworkCore;
using Sentinel.Api.Data;
using Sentinel.Api.Data.Seeds;

namespace Sentinel.Api
{
    public class Worker(
        IServiceScopeFactory scopeFactory,
        ILogger<Worker> logger
        ) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Starting database migration and seeding...");
            using var scope = scopeFactory.CreateScope();
            var sentinelDbContext = scope.ServiceProvider.GetRequiredService<SentinelDbContext>();

            await sentinelDbContext.Database.MigrateAsync();
            logger.LogInformation("Database migration completed.");

            var seeders = scope.ServiceProvider.GetServices<ISeedService>().ToList();
            foreach (var seeder in seeders)
            {
                try
                {
                    logger.LogInformation("Seeding data using {Seeder}...", seeder.GetType().Name);
                    await seeder.SeedAsync();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error occurred while seeding data using {Seeder}.", seeder.GetType().Name);
                }
            }
        }
    }
}