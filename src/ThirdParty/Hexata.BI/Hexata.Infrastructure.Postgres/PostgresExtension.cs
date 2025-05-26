using Hexata.BI.Application.Repositories;
using Hexata.Infrastructure.Postgres.Respositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hexata.Infrastructure.Postgres
{
    public static class PostgresExtension
    {
        public static IHostApplicationBuilder AddPostgres(this IHostApplicationBuilder builder)
        {
            var configuration = builder.Configuration;
            var connectionString = configuration.GetSection("ConnectionStrings:SqlBiConnection");

            builder.Services.AddDbContext<HexataDbContext>(options =>
                options.UseNpgsql(connectionString.Value));

            builder.Services.AddScoped<IBISaleRepository, BISaleRepository>();
            builder.Services.AddScoped<IBICustomerRepository, BICustomerRepository>();

            return builder;
        }

        public static void ApplyPostgresMigrations(this IHost app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<HexataDbContext>();
            dbContext.Database.Migrate();
        }
    }
}