using BuildingBlock.Application.Extensions;
using BuildingBlock.Domain.ValueObject.Json.Extensions;
using BuildingBlocks.Api.Extensions;
using BuildingBlocks.Observability.ElasticStack.Extensions;

namespace Administration.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            builder.AddObservability(configuration.GetSection("Observability").Bind);

            builder.Services.AddControllers()
                .AddNewtonsoftJson();

            builder.Services.AddApplicationDomainData();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddApplicationValidation();
            builder.Services.AddApplicationSwagger();


            var app = builder.Build();

            app.UseObservability();
            app.UseApplicationSwagger();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();



            app.Run();
        }
    }
}

