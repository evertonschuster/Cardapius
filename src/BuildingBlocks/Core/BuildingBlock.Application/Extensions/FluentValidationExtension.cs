using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Application.Extensions
{
    public static class FluentValidationExtension
    {
        public static void AddApplicationValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
        }
    }
}
