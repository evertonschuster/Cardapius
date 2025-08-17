using Microsoft.AspNetCore.DataProtection;
using OpenIddict.Abstractions;
using OpenIddict.Validation.AspNetCore;
using Sentinel.Api.Data;

namespace Sentinel.Api.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddAppAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);

            services.AddOpenIddict()
                .AddCore(opt =>
                {
                    opt.UseEntityFrameworkCore().UseDbContext<SentinelDbContext>();
                })
                .AddServer(opt =>
                {
                    var enablePasswordFlow = configuration.GetValue("Authentication:EnablePasswordFlow", false);
                    var useDevCertificates = configuration.GetValue("Authentication:UseDevelopmentCertificates", false);

                    opt.SetAuthorizationEndpointUris("/connect/authorize")
                       .SetTokenEndpointUris("/connect/token")
                       .SetIntrospectionEndpointUris("/connect/introspect")
                       .SetRevocationEndpointUris("/connect/revocation")
                       .AllowAuthorizationCodeFlow()
                       .AllowRefreshTokenFlow()
                       .AcceptAnonymousClients()
                       .RequireProofKeyForCodeExchange();

                    if (enablePasswordFlow)
                    {
                        opt.AllowPasswordFlow();
                    }
                    if (useDevCertificates)
                    {
                        opt.AddDevelopmentEncryptionCertificate()
                           .AddDevelopmentSigningCertificate();
                    }

                    opt.UseAspNetCore()
                           .EnableAuthorizationEndpointPassthrough()
                           .EnableTokenEndpointPassthrough();
                    //.EnableIntrospectionEndpointPassthrough()
                    //.EnableRevocationEndpointPassthrough();

                    opt.RegisterScopes(
                       OpenIddictConstants.Scopes.Email,
                       OpenIddictConstants.Scopes.Profile,
                       OpenIddictConstants.Scopes.OpenId,
                       OpenIddictConstants.Scopes.OfflineAccess,
                       "api" // Your custom API scope
                   );
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

            
            var keyRingPath = configuration["DataProtection:KeyRingPath"] ?? Path.Combine(AppContext.BaseDirectory, "keys");
            Directory.CreateDirectory(keyRingPath);
            services
                    .AddDataProtection()
                    .PersistKeysToFileSystem(new DirectoryInfo(keyRingPath));

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.AddAuthenticationSchemes(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.RequireAssertion(ctx =>
                        ctx.User.Claims.Any(c => c.Type == OpenIddictConstants.Claims.Scope &&
                                                 c.Value.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                                        .Contains("api")));
                });
            });

            return services;
        }


    }
}
