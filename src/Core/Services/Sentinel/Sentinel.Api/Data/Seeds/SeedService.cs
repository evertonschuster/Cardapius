using Microsoft.EntityFrameworkCore;

namespace Sentinel.Api.Data.Seeds
{
    public static class SeedService
    {
        public static async Task SeedAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<SentinelDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<SentinelDbContext>>();
            logger.LogInformation("Starting database migration and seeding...");


            await context.Database.MigrateAsync();
            logger.LogInformation("Database migration completed.");

            var seeders = scope.ServiceProvider.GetRequiredService<IEnumerable<ISeedService>>() ?? [];
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
