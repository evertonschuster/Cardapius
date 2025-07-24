using BuildingBlock.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Infra.DataBase.Extensions
{
    public static class DomainEventExtensions
    {
        public static void AddDomainEvent(this IServiceCollection services)
        {
            services.AddSingleton<IDomainEventService, DomainEventService>();
        }
    }
}
