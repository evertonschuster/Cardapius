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

public class AuthorizationController : Controller
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

    public AuthorizationController(
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

        var user = await _userManager.GetUserAsync(User);
        if (user is null || !user.IsActive || (user.AccessGrantedUntil.HasValue && user.AccessGrantedUntil < DateTime.UtcNow)
            || !await _signInManager.CanSignInAsync(user))
        {
            await _signInManager.SignOutAsync();
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
}
