using Administration.Application.Products.Commands.CreateProduct;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Administration.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            services.AddScoped<IValidator<CreateProductCommand>, CreateProductValidator>();

            return services;
        }
    }
}
