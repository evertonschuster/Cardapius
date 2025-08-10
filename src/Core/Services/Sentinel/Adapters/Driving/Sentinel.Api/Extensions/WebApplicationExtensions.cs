using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using Sentinel.Api.Data;
using Sentinel.Api.Middleware;

namespace Sentinel.Api.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseSentinel(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        app.UseCorrelationId();
        app.UseRateLimiter();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "Sentinel API v1");
                o.OAuthClientId("swagger");
                o.OAuthUsePkce();
            });
        }

        app.MapControllers();
        app.MapHealthChecks("/health");
        return app;
    }

    public static async Task SeedAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<SentinelDbContext>();
        await context.Database.MigrateAsync();

        var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        if (await manager.FindByClientIdAsync("console") is null)
        {
            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "console",
                ClientSecret = "secret",
                Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Token,
                    OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                    OpenIddictConstants.Permissions.GrantTypes.Password,
                    OpenIddictConstants.Permissions.Prefixes.Scope + "api"
                }
            });
        }

        if (await manager.FindByClientIdAsync("swagger") is null)
        {
            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "swagger",
                RedirectUris = { new Uri("https://localhost:5001/swagger/oauth2-redirect.html") },
                Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Authorization,
                    OpenIddictConstants.Permissions.Endpoints.Token,
                    OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                    OpenIddictConstants.Permissions.ResponseTypes.Code,
                    OpenIddictConstants.Permissions.Prefixes.Scope + "api"
                },
                Requirements = { OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange }
            });
        }

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var rolesSection = app.Configuration.GetSection("Roles");
        foreach (var serviceRoles in rolesSection.GetChildren())
        {
            foreach (var role in serviceRoles.Get<string[]>() ?? Array.Empty<string>())
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
