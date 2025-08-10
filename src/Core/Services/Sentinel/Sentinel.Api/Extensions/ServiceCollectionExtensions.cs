using System.IO;
using System.Threading.RateLimiting;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using OpenIddict.Validation.AspNetCore;
using Sentinel.Api.Data;

namespace Sentinel.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSentinelServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SentinelDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Default"));
            options.UseOpenIddict();
        });

        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<SentinelDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);

        services.AddOpenIddict()
            .AddCore(opt =>
            {
                opt.UseEntityFrameworkCore().UseDbContext<SentinelDbContext>();
            })
            .AddServer(opt =>
            {
                opt.SetAuthorizationEndpointUris("/connect/authorize")
                   .SetTokenEndpointUris("/connect/token")
                   .SetIntrospectionEndpointUris("/connect/introspect")
                   .SetRevocationEndpointUris("/connect/revocation")
                   .AllowAuthorizationCodeFlow()
                   .AllowRefreshTokenFlow()
                   .AllowClientCredentialsFlow()
                   .AllowPasswordFlow()
                   .RequireProofKeyForCodeExchange()
                   .AddDevelopmentEncryptionCertificate()
                   .AddDevelopmentSigningCertificate()
                   .UseAspNetCore()
                       .EnableAuthorizationEndpointPassthrough()
                       .EnableTokenEndpointPassthrough();
                       //.EnableIntrospectionEndpointPassthrough()
                       //.EnableRevocationEndpointPassthrough();
            })
            .AddValidation(opt =>
            {
                var issuer = configuration["Jwt:Issuer"];
                var audience = configuration["Jwt:Audience"];
                if (!string.IsNullOrEmpty(issuer))
                    opt.SetIssuer(new Uri(issuer));
                if (!string.IsNullOrEmpty(audience))
                    opt.AddAudiences(audience);
                opt.UseLocalServer();
                opt.UseAspNetCore();
            });

        services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(AppContext.BaseDirectory, "keys")));

        services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
            {
                if (context.Request.Path.StartsWithSegments("/connect/token"))
                {
                    return RateLimitPartition.GetFixedWindowLimiter("token", _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 10,
                        Window = TimeSpan.FromMinutes(1),
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 2
                    });
                }

                return RateLimitPartition.GetNoLimiter("none");
            });
        });

        services.AddCors();
        services.AddControllers();
        services.AddFluentValidationAutoValidation();
        services.AddEndpointsApiExplorer();
        services.AddAuthorization(options =>
        {
            options.AddPolicy("ApiScope", policy =>
                policy.RequireClaim(OpenIddictConstants.Claims.Scope, "api"));
        });
        services.AddSwaggerGen(setup =>
        {
            setup.SwaggerDoc("v1", new() { Title = "Sentinel API", Version = "v1" });
        });
        services.AddHealthChecks();

        return services;
    }
}
