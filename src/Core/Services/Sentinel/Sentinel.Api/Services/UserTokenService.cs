using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using Sentinel.Api.Models;

namespace Sentinel.Api.Services;

public interface IUserTokenService
{
    Task<ApplicationUser?> ValidateUserAsync(string username, string password);
    Task<ApplicationUser?> ValidateUserAsync(ClaimsPrincipal principal);
    Task SignOutAsync();
    Task<ClaimsPrincipal> CreatePrincipalAsync(ApplicationUser user, IEnumerable<string> requestedScopes, string? clientId);
}

public class UserTokenService(
    SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager,
    IClientConfigurationService clientConfig) : IUserTokenService
{
    public async Task<ApplicationUser?> ValidateUserAsync(string username, string password)
    {
        var user = await userManager.FindByNameAsync(username);
        if (user is null || !await IsValidAsync(user))
            return null;

        var result = await signInManager.CheckPasswordSignInAsync(user, password, true);
        return result.Succeeded ? user : null;
    }

    public async Task<ApplicationUser?> ValidateUserAsync(ClaimsPrincipal principal)
    {
        var user = await userManager.GetUserAsync(principal);
        return await IsValidAsync(user) ? user : null;
    }

    public Task SignOutAsync() => signInManager.SignOutAsync();

    public async Task<ClaimsPrincipal> CreatePrincipalAsync(ApplicationUser user, IEnumerable<string> requestedScopes, string? clientId)
    {
        var principal = await signInManager.CreateUserPrincipalAsync(user);
        principal.SetClaim(OpenIddictConstants.Claims.Subject, user.Id);

        var allowedScopes = clientConfig.GetAllowedScopes(clientId);
        var scopes = requestedScopes.Intersect(allowedScopes);
        principal.SetScopes(scopes);

        foreach (var claim in principal.Claims)
        {
            claim.SetDestinations(OpenIddictConstants.Destinations.AccessToken, OpenIddictConstants.Destinations.IdentityToken);
        }

        var lifetimes = clientConfig.GetTokenLifetimes(clientId);
        if (lifetimes.AccessToken.HasValue)
            principal.SetAccessTokenLifetime(lifetimes.AccessToken.Value);
        if (lifetimes.RefreshToken.HasValue)
            principal.SetRefreshTokenLifetime(lifetimes.RefreshToken.Value);
        if (lifetimes.AuthorizationCode.HasValue)
            principal.SetAuthorizationCodeLifetime(lifetimes.AuthorizationCode.Value);

        return principal;
    }

    private async Task<bool> IsValidAsync(ApplicationUser? user)
    {
        return user is not null &&
               user.IsActive &&
               (!user.AccessGrantedUntil.HasValue || user.AccessGrantedUntil >= DateTime.UtcNow) &&
               await signInManager.CanSignInAsync(user);
    }
}
