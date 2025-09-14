using OpenIddict.Abstractions;
using System.Globalization;
using static OpenIddict.Abstractions.OpenIddictConstants;

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
                    ClientId = "SPA",
                    //ClientSecret = "secret",
                    ClientType = ClientTypes.Public,
                    Permissions =
                    {
                        Permissions.Endpoints.Authorization,
                        Permissions.Endpoints.Token,
                        Permissions.Endpoints.Revocation,
                        Permissions.Endpoints.EndSession,
                        
                        Permissions.GrantTypes.AuthorizationCode,
                        Permissions.GrantTypes.RefreshToken,
                        Permissions.GrantTypes.ClientCredentials,

                        Permissions.ResponseTypes.Code,
                        
                        Claims.Private.PostLogoutRedirectUri,

                        Permissions.Scopes.Profile,

                        Scopes.OfflineAccess
                    },
                    RedirectUris = {
                        new Uri("https://localhost:5001/swagger/oauth2-redirect.html"),
                        new Uri("http://localhost:3000/callback"),
                        new Uri("http://localhost:3000/silent-renew")
                    },
                    PostLogoutRedirectUris =
                    {
                        new Uri("http://localhost:3000/login")
                    },
                    Requirements =
                    {
                        Requirements.Features.ProofKeyForCodeExchange
                    }
                };


                descriptor.Settings[Settings.TokenLifetimes.AccessToken] = TimeSpan.FromMinutes(2).ToString("c", CultureInfo.InvariantCulture);
                descriptor.Settings[Settings.TokenLifetimes.IdentityToken] = TimeSpan.FromMinutes(2).ToString("c", CultureInfo.InvariantCulture);
                descriptor.Settings[Settings.TokenLifetimes.RefreshToken] = TimeSpan.FromMinutes(4).ToString("c", CultureInfo.InvariantCulture);
                descriptor.Settings[Settings.TokenLifetimes.AuthorizationCode] = TimeSpan.FromMinutes(2).ToString("c", CultureInfo.InvariantCulture);

                foreach (var scope in DefaultAllowedScopes)
                {
                    descriptor.Permissions.Add(OpenIddictConstants.Permissions.Prefixes.Scope + scope);
                }

                await manager.CreateAsync(descriptor);
            }
        }
    }
}
