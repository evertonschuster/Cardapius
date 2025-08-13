using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using Sentinel.Api.Models;

namespace Sentinel.Api.Controllers;

public class AuthorizationController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthorizationController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpPost("~/connect/token")]
    [IgnoreAntiforgeryToken]
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
        else if (request.GrantType == OpenIddictConstants.GrantTypes.AuthorizationCode ||
                 request.GrantType == OpenIddictConstants.GrantTypes.RefreshToken)
        {
            return Forbid(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        throw new InvalidOperationException("The specified grant type is not supported.");
    }
}
