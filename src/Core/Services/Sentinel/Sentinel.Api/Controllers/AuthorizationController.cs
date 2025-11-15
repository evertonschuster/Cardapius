using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using Sentinel.Api.Models;
using Sentinel.Api.Services;

namespace Sentinel.Api.Controllers;

public class AuthorizationController(IUserTokenService tokenService, SignInManager<ApplicationUser> _signInManager) : Controller
{
    [IgnoreAntiforgeryToken]
    [HttpGet("~/connect/authorize")]
    public async Task<IActionResult> Authorize()
    {
        var request = HttpContext.GetOpenIddictServerRequest() ?? throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

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

    [Authorize]
    [HttpPost("~/connect/logout")]
    [HttpGet("~/connect/logout")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Logout(string? returnUrl = null, string? post_logout_redirect_uri = null)
    {
        await _signInManager.SignOutAsync();

        var redirect = post_logout_redirect_uri;

        return SignOut(
            authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
            properties: new AuthenticationProperties
            {
                RedirectUri = post_logout_redirect_uri
            });
    }
}