using BuildingBlock.Application.Extensions;
using BuildingBlock.Domain.ValueObject.Json.Extensions;
using BuildingBlocks.Api.Extensions;

namespace Administration.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddApplicationDomainData();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddApplicationValidation();
            builder.Services.AddApplicationSwagger();



            var app = builder.Build();

            app.UseApplicationSwagger();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();



            app.Run();
        }
    }
}

