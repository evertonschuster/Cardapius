using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using Sentinel.Api.Models;
using System.Security.Claims;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Sentinel.Api.Controllers;

public class TokenController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private static readonly string[] DefaultAllowedScopes =
    {
        OpenIddictConstants.Scopes.Email,
        OpenIddictConstants.Scopes.Profile,
        OpenIddictConstants.Scopes.OpenId,
        OpenIddictConstants.Scopes.OfflineAccess,
        "api"
    };

    public TokenController(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
    }

    private string[] GetAllowedScopes(string? clientId)
    {
        if (string.IsNullOrEmpty(clientId))
        {
            return DefaultAllowedScopes;
        }

        var scopes = _configuration.GetSection($"Clients:{clientId}:AllowedScopes").Get<string[]>() ?? [];
        return scopes.Length > 0 ? scopes : DefaultAllowedScopes;
    }

    [IgnoreAntiforgeryToken]
    [HttpPost("~/connect/token")]
    public async Task<IActionResult> Exchange()
    {
        var request = HttpContext.GetOpenIddictServerRequest() ??
                      throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

        if (request.GrantType == OpenIddictConstants.GrantTypes.Password)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user is null)
            {
                return Forbid(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            if (!user.IsActive || (user.AccessGrantedUntil.HasValue && user.AccessGrantedUntil < DateTime.UtcNow))
            {
                return Forbid(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password!, true);
            if (!result.Succeeded || !await _signInManager.CanSignInAsync(user))
            {
                return Forbid(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            var principal = await _signInManager.CreateUserPrincipalAsync(user);
            principal.SetClaim(OpenIddictConstants.Claims.Subject, user.Id);
            var scopes = request.GetScopes().Intersect(GetAllowedScopes(request.ClientId));
            principal.SetScopes(scopes);
            foreach (var claim in principal.Claims.Where(c => c.Type != ClaimTypes.SecurityStamp))
            {
                claim.SetDestinations(OpenIddictConstants.Destinations.AccessToken, OpenIddictConstants.Destinations.IdentityToken);
            }

            return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
        else if (request.GrantType is OpenIddictConstants.GrantTypes.AuthorizationCode or OpenIddictConstants.GrantTypes.RefreshToken)
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

            var principal = authenticateResult?.Principal;
            if (principal is null)
                return Forbid(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

            var userId = principal.GetClaim(OpenIddictConstants.Claims.Subject);
            if (string.IsNullOrEmpty(userId))
                return Forbid(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null || !user.IsActive
                || (user.AccessGrantedUntil.HasValue && user.AccessGrantedUntil < DateTime.UtcNow)
                || !await _signInManager.CanSignInAsync(user))
            {
                return Forbid(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            principal.SetScopes(principal.GetScopes().Intersect(GetAllowedScopes(request.ClientId)));
            foreach (var claim in principal.Claims.Where(c => c.Type != ClaimTypes.SecurityStamp))
                claim.SetDestinations(OpenIddictConstants.Destinations.AccessToken,
                                      OpenIddictConstants.Destinations.IdentityToken);

            return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        throw new InvalidOperationException("The specified grant type is not supported.");
    }
}
