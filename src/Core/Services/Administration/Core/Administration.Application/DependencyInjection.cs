using Administration.Application.Products.Commands.CreateProduct;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Administration.Application
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers MediatR and application-specific services, including validators, into the dependency injection container.
        /// </summary>
        /// <returns>The updated <see cref="IServiceCollection"/> with application services registered.</returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            services.AddScoped<IValidator<CreateProductCommand>, CreateProductValidator>();

            return services;
        }
    }
}
