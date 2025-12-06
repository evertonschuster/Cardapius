using Administration.Application.Products.Commands.CreateProduct;
using Administration.Application.Ncms.Commands.CreateNcm;
using Administration.Application.Ncms.Commands.UpdateNcm;
using Administration.Application.Ncms.Queries.GetNcmById;
using Administration.Application.Ncms.Queries.ListNcms;
using Administration.Application.Suppliers.Commands.CreateSupplier;
using Administration.Application.Suppliers.Commands.UpdateSupplier;
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
            services.AddScoped<IValidator<CreateNcmCommand>, CreateNcmValidator>();
            services.AddScoped<IValidator<CreateSupplierCommand>, CreateSupplierValidator>();
            services.AddScoped<IValidator<UpdateSupplierCommand>, UpdateSupplierValidator>();
            services.AddScoped<IValidator<UpdateNcmCommand>, UpdateNcmValidator>();
            services.AddScoped<IValidator<GetNcmByIdQuery>, GetNcmByIdValidator>();
            services.AddScoped<IValidator<ListNcmsQuery>, ListNcmsValidator>();

            return services;
        }
    }
}