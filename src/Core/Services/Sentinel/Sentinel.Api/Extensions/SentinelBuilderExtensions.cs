using BuildingBlock.Observability.OpenTelemetry.Extensions;
using FluentValidation.AspNetCore;

namespace Sentinel.Api.Extensions;

public static class SentinelBuilderExtensions
{
    public static WebApplicationBuilder AddSentinel(this WebApplicationBuilder builder)
    {
        builder.AddObservability();
        builder.Host.AddAppLogging(builder.Configuration);
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        builder.Services.AddDatabase(builder.Configuration);
        builder.Services.AddIdentity();
        builder.Services.AddAppAuthentication(builder.Configuration);

        builder.Services.AddServices();
        builder.Services.AddAppRateLimiter(builder.Configuration);

        builder.Services.AddCors();
        builder.Services.AddControllers();
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddAppSwagger();
        builder.Services.AddHealthChecks();

        return builder;
    }
}
