using BuildingBlock.Infra.DataBase.EntityFramework.Interceptors;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Infra.DataBase.EntityFramework.Extensions
{
    public static class ServiceRegistreitorExtensions
    {
        public static void AddDbContextServices(this IServiceCollection services)
        {
            services.AddScoped<AuditingInterceptor>();
            services.AddScoped<EmitDomainEventInterceptor>();
            services.AddScoped<SoftDeleteInterceptor>();

            services.AddScoped<DbContextContainer>();
        }
    }
}
