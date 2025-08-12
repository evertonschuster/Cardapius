
using OpenIddict.Abstractions;

namespace Sentinel.Api.Data.Seeds
{
    public class ClientSeedService(IOpenIddictApplicationManager manager) : ISeedService
    {
        public async Task SeedAsync()
        {
            if (await manager.FindByClientIdAsync("console") is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "console",
                    ClientSecret = "secret",
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Authorization,
                        OpenIddictConstants.Permissions.Endpoints.Token,
                        OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                        OpenIddictConstants.Permissions.ResponseTypes.Code,
                        OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                        OpenIddictConstants.Permissions.GrantTypes.Password,
                        OpenIddictConstants.Permissions.Scopes.Email,
                        OpenIddictConstants.Permissions.Scopes.Profile,
                        OpenIddictConstants.Scopes.OpenId,
                        OpenIddictConstants.Scopes.OfflineAccess,
                        OpenIddictConstants.Permissions.Prefixes.Scope + "api"
                    },
                    RedirectUris = { new Uri("https://localhost:5001/swagger/oauth2-redirect.html") },
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
        }
    }
}
