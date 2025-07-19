using BuildingBlock.Api.Application.Extensions;
using BuildingBlock.Api.Domain.ValueObjects.Json.Extensions;


//using BuildingBlock.Api.Domain.ValueObjects.Json.Extensions;
using BuildingBlock.Api.Swashbuckle.Extensions;
using BuildingBlock.Api.Version.Extensions;
using BuildingBlock.Observability.OpenTelemetry.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

namespace BuildingBlock.Api.Extensions;

// Adds common .NET Aspire services: service discovery, resilience, health checks, and OpenTelemetry.
// This project should be referenced by each service project in your solution.
// To learn more about using this project, see https://aka.ms/dotnet/aspire/service-defaults
public static class Extensions
{
    public static TBuilder AddServiceDefaults<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        builder.AddObservability();

        builder.AddDefaultHealthChecks();

        builder.Services.AddProblemDetails();
        builder.Services.AddServiceDiscovery();


        //Microsoft
        builder.Services
            .AddControllers()
            .AddNewtonsoftJson();

        builder.Services.AddEndpointsApiExplorer();

        //BuildingBlocks
        builder.Services.AddApplicationDomainDataJsonConvert();
        builder.Services.AddApplicationValidation();
        builder.Services.AddApplicationVersion();
        builder.Services.AddApplicationSwagger();

        builder.Services.ConfigureHttpClientDefaults(http =>
        {
            // Turn on resilience by default
            http.AddStandardResilienceHandler();

            // Turn on service discovery by default
            http.AddServiceDiscovery();
        });

        // Uncomment the following to restrict the allowed schemes for service discovery.
        // builder.Services.Configure<ServiceDiscoveryOptions>(options =>
        // {
        //     options.AllowedSchemes = ["https"];
        // });

        return builder;
    }

    public static TBuilder AddDefaultHealthChecks<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        builder.Services.AddHealthChecks()
            // Add a default liveness check to ensure app is responsive
            .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

        return builder;
    }

    public static WebApplication UseServiceDefaults(this WebApplication app)
    {
        app.UseApplicationSwagger();
        app.UseDefaultEndpoints();
        return app;
    }

    public static WebApplication UseDefaultEndpoints(this WebApplication app)
    {
        app.MapControllers();
        // Adding health checks endpoints to applications in non-development environments has security implications.
        // See https://aka.ms/dotnet/aspire/healthchecks for details before enabling these endpoints in non-development environments.
        if (app.Environment.IsDevelopment())
        {
            // All health checks must pass for app to be considered ready to accept traffic after starting
            app.MapHealthChecks("/health");

            // Only health checks tagged with the "live" tag must pass for app to be considered alive
            app.MapHealthChecks("/alive", new HealthCheckOptions
            {
                Predicate = r => r.Tags.Contains("live")
            });
        }

        return app;
    }
}
