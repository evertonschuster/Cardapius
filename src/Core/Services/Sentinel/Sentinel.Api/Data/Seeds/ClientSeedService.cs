using OpenIddict.Abstractions;
using System.Globalization;

namespace Sentinel.Api.Data.Seeds
{
    public class ClientSeedService(IOpenIddictApplicationManager manager) : ISeedService
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
            if (await manager.FindByClientIdAsync("console") is null)
            {
                var descriptor = new OpenIddictApplicationDescriptor
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
                    },
                    RedirectUris = { new Uri("https://localhost:5001/swagger/oauth2-redirect.html") },
                };


                descriptor.Settings[OpenIddictConstants.Settings.TokenLifetimes.AccessToken] = TimeSpan.FromMinutes(20).ToString("c", CultureInfo.InvariantCulture);
                descriptor.Settings[OpenIddictConstants.Settings.TokenLifetimes.IdentityToken] = TimeSpan.FromMinutes(20).ToString("c", CultureInfo.InvariantCulture);
                descriptor.Settings[OpenIddictConstants.Settings.TokenLifetimes.RefreshToken] = TimeSpan.FromMinutes(120).ToString("c", CultureInfo.InvariantCulture);
                descriptor.Settings[OpenIddictConstants.Settings.TokenLifetimes.AuthorizationCode] = TimeSpan.FromMinutes(5).ToString("c", CultureInfo.InvariantCulture);

                foreach (var scope in DefaultAllowedScopes)
                {
                    descriptor.Permissions.Add(OpenIddictConstants.Permissions.Prefixes.Scope + scope);
                }

                await manager.CreateAsync(descriptor);
            }
        }
    }
}
