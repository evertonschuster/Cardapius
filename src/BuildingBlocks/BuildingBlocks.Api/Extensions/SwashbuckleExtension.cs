using BuildingBlock.Domain.ValueObjects.Emails;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace BuildingBlocks.Api.Extensions
{
    public static class SwashbuckleExtension
    {
        public static void AddApplicationSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                //TODO: Auto registrarion with ioc
                options.MapType<Email>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString(Email.Empty),
                });
            });
        }

        public static void UseApplicationSwagger(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }
    }
}
