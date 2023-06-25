using Administration.Domain.Restaurants.Repositories;
using Administration.Infra.DataBase.EntityFramework.Restaurants.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Administration.Infra.DataBase.EntityFramework.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddInfraDataBaseEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            var conectionString = configuration.GetConnectionString("AdministrationDb");
            services.AddDbContext<AdministrationDbContext>(options => options.UseNpgsql(conectionString));

            services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetRequiredService<AdministrationDbContext>());

            //services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<AdministrationDbContext>());

            services.AddScoped<IRestaurantRepository, RestaurantRepository>();

            return services;
        }
    }
}
