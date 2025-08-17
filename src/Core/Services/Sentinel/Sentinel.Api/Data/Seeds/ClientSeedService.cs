using OpenIddict.Abstractions;
using Microsoft.Extensions.Configuration;

namespace Sentinel.Api.Data.Seeds
{
    public class ClientSeedService(IOpenIddictApplicationManager manager, IConfiguration configuration) : ISeedService
    {
        private static readonly string[] DefaultAllowedScopes =
        {
            OpenIddictConstants.Scopes.Email,
            OpenIddictConstants.Scopes.Profile,
            OpenIddictConstants.Scopes.OpenId,
            OpenIddictConstants.Scopes.OfflineAccess,
            "api"
        };

        public async Task SeedAsync()
        {
            var consoleSection = configuration.GetSection("Clients:Console");
            var clientSecret = consoleSection["Secret"];
            if (string.IsNullOrWhiteSpace(clientSecret))
            {
                throw new InvalidOperationException("Client secret for 'console' is not configured.");
            }

            var allowedScopes = consoleSection.GetSection("AllowedScopes").Get<string[]>() ?? DefaultAllowedScopes;

            if (await manager.FindByClientIdAsync("console") is null)
            {
                var descriptor = new OpenIddictApplicationDescriptor
                {
                    ClientId = "console",
                    ClientSecret = clientSecret,
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Authorization,
                        OpenIddictConstants.Permissions.Endpoints.Token,
                        OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                        OpenIddictConstants.Permissions.ResponseTypes.Code,
                        OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                        OpenIddictConstants.Permissions.GrantTypes.Password
                    },
                    RedirectUris = { new Uri("https://localhost:5001/swagger/oauth2-redirect.html") },
                };

                foreach (var scope in allowedScopes)
                {
                    descriptor.Permissions.Add(OpenIddictConstants.Permissions.Prefixes.Scope + scope);
                }

                await manager.CreateAsync(descriptor);
            }
        }
    }
}
