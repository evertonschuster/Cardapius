using BuildingBlock.Domain.ValueObjects.Emails;
using BuildingBlock.Domain.ValueObjects.PersonNames;
using BuildingBlock.Domain.ValueObjects.Phones;
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
            //Add BuildingBlocks for swagger
            services.AddSwaggerGen(options =>
            {
                options.MapType<Email>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString(Email.Empty),
                });
                options.MapType<PersonName>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString(PersonName.Empty),
                });
                options.MapType<Phone>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString(Phone.Empty),
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
