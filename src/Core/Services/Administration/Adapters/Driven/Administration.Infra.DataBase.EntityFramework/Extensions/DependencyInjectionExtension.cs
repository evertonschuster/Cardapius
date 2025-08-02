using Administration.Domain.Products.Repositories;
using Administration.Domain.Restaurants.Repositories;
using Administration.Infra.DataBase.EntityFramework.Products;
using Administration.Infra.DataBase.EntityFramework.Restaurants.Repositories;
using BuildingBlock.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Administration.Infra.DataBase.EntityFramework.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddInfraDataBaseEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            var conectionString = configuration.GetConnectionString("AdministrationDb");

            services.AddDbContext<AdministrationDbContext>((serviceProvider, options) =>
            {
                var env = serviceProvider.GetRequiredService<IHostEnvironment>();
                options.UseNpgsql(conectionString);
                if (env.IsDevelopment())
                {
                    options.EnableSensitiveDataLogging();
                    options.EnableDetailedErrors();
                }
            });


            services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetRequiredService<AdministrationDbContext>());
            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<AdministrationDbContext>());

            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
