using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlock.Api.Application.Extensions
{
    public static class MediatRExtensions
    {
        /// <summary>
        /// Registers MediatR services by scanning and adding handlers from the executing assembly to the service collection.
        /// </summary>
        public static void AddApplicationMediatr(this IServiceCollection services)
        {
            services.AddMediatR(x => x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
