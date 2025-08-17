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
                    opt.SetAuthorizationEndpointUris("/connect/authorize")
                       .SetTokenEndpointUris("/connect/token")
                       .SetIntrospectionEndpointUris("/connect/introspect")
                       .SetRevocationEndpointUris("/connect/revocation")
                       .AllowAuthorizationCodeFlow()
                       .AllowPasswordFlow()
                       .AllowRefreshTokenFlow()
                       .AllowClientCredentialsFlow()
                       .AcceptAnonymousClients()
                       .RequireProofKeyForCodeExchange()
                       .AddDevelopmentEncryptionCertificate()
                       .AddDevelopmentSigningCertificate();

                    var lifetimes = configuration.GetSection("OpenIddict:TokenLifetimes");
                    var access = lifetimes.GetValue<int?>("AccessToken");
                    if (access.HasValue)
                        opt.SetAccessTokenLifetime(TimeSpan.FromMinutes(access.Value));
                    var refresh = lifetimes.GetValue<int?>("RefreshToken");
                    if (refresh.HasValue)
                        opt.SetRefreshTokenLifetime(TimeSpan.FromMinutes(refresh.Value));
                    var code = lifetimes.GetValue<int?>("AuthorizationCode");
                    if (code.HasValue)
                        opt.SetAuthorizationCodeLifetime(TimeSpan.FromMinutes(code.Value));

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

            services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(AppContext.BaseDirectory, "keys")));
            services.AddAuthorization(options =>
            {
                //options.AddPolicy("ApiScope", policy =>
                //    policy.RequireClaim(OpenIddictConstants.Claims.Scope, "api"));
            });

            return services;
        }


    }
}
