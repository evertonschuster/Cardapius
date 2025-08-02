using BuildingBlock.Infra.DataBase.EntityFramework.Interceptors;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Infra.DataBase.EntityFramework.Extensions
{
    public static class ServiceRegistreitorExtensions
    {
        /// <summary>
        /// Registers Entity Framework interceptors and the DbContext container with scoped lifetimes in the dependency injection container.
        /// </summary>
        public static void AddDbContextServices(this IServiceCollection services)
        {
            services.AddScoped<AuditingInterceptor>();
            services.AddScoped<EmitDomainEventInterceptor>();
            services.AddScoped<SoftDeleteInterceptor>();

            services.AddScoped<DbContextContainer>();
        }
    }
}
