using BuildingBlock.Application;
using BuildingBlock.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Infra.DataBase.Extensions
{
    public static class ApplicationExtensions
    {
        public static void AddDomainEvent(this IServiceCollection services)
        {
            services.AddSingleton<IDomainEventService, DomainEventService>();
        }

        public static void AddUserContext(this IServiceCollection services)
        {
            services.AddScoped<IUserContext, UserContext>();
        }
    }
}
