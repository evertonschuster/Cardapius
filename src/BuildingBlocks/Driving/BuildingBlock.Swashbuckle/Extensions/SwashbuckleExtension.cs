using BuildingBlock.Swashbuckle.Domain.ValueObjects.Map;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BuildingBlock.Swashbuckle.Extensions
{
    public static class SwashbuckleExtension
    {
        public static void AddApplicationSwagger(this IServiceCollection services)
        {
            //Add BuildingBlocks for swagger
            services.AddSwaggerGen(options =>
            {
                options.AddDomainMapping();
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
