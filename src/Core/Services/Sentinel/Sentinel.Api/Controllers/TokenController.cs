using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using Sentinel.Api.Models;

namespace Sentinel.Api.Controllers;

public class TokenController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public TokenController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

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
            if (!result.Succeeded)
            {
                return Forbid(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            var principal = await _signInManager.CreateUserPrincipalAsync(user);
            principal.SetClaim(OpenIddictConstants.Claims.Subject, user.Id);
            principal.SetScopes(request.GetScopes());
            foreach (var claim in principal.Claims)
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

            foreach (var claim in principal.Claims)
                claim.SetDestinations(OpenIddictConstants.Destinations.AccessToken,
                                      OpenIddictConstants.Destinations.IdentityToken);

            // Importante: não recrie o principal aqui – reutilize o que veio do OpenIddict
            // para preservar autorização, scopes e presenters.
            return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        throw new InvalidOperationException("The specified grant type is not supported.");
    }
}
