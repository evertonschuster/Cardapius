using BuildingBlock.Application;
using BuildingBlock.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace BuildingBlock.Infra.DataBase.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ApplicationExtensions
    {
        /// <summary>
        /// Registers the domain event service as a singleton in the dependency injection container.
        /// </summary>
        public static void AddDomainEvent(this IServiceCollection services)
        {
            services.AddSingleton<IDomainEventService, DomainEventService>();
        }

        /// <summary>
        /// Registers the <see cref="IUserContext"/> service with a scoped lifetime using the <see cref="UserContext"/> implementation.
        /// </summary>
        public static void AddUserContext(this IServiceCollection services)
        {
            services.AddScoped<IUserContext, UserContext>();
        }
    }
}
