using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using Sentinel.Api.Services;

namespace Sentinel.Api.Controllers;

public class AuthorizationController(IUserTokenService tokenService) : Controller
{
    [IgnoreAntiforgeryToken]
    [HttpGet("~/connect/authorize")]
    public async Task<IActionResult> Authorize()
    {
        var request = HttpContext.GetOpenIddictServerRequest() ??
                      throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

        if (!User.Identity?.IsAuthenticated ?? true)
        {
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = Request.PathBase + Request.Path + QueryString.Create(Request.Query.ToList())
            });
        }

        var user = await tokenService.ValidateUserAsync(User);
        if (user is null)
        {
            await tokenService.SignOutAsync();
            return Forbid(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        var principal = await tokenService.CreatePrincipalAsync(user, request.GetScopes(), request.ClientId);
        return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }
}
