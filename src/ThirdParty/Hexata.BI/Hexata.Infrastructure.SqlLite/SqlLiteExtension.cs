using Hexata.BI.Application.Repositories;
using Hexata.Infrastructure.SqlLite.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hexata.Infrastructure.SqlLite
{
    public static class SqlLiteExtension
    {
        public static void AddSqlLite(this IHostApplicationBuilder builder)
        {
            var configuration = builder.Configuration.GetConnectionString("SqlLiteConnection");
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(configuration);
            });

            builder.Services.AddScoped<IServiceStateRepository, ServiceStateRepository>();
            builder.Services.AddScoped<IMonthlyConsumptionRepository, MonthlyConsumptionRepository>();
        }

        public static void ApplyMigrations(this IHost app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();
        }
    }
}
