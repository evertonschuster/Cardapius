using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using Sentinel.Api.Services;

namespace Sentinel.Api.Controllers;

public class TokenController(IUserTokenService tokenService) : Controller
{
    [IgnoreAntiforgeryToken]
    [HttpPost("~/connect/token")]
    public async Task<IActionResult> Exchange()
    {
        var request = HttpContext.GetOpenIddictServerRequest() ??
                      throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

        switch (request.GrantType)
        {
            case OpenIddictConstants.GrantTypes.Password:
            {
                var user = await tokenService.ValidateUserAsync(request.Username!, request.Password!);
                if (user is null)
                    return Forbid(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

                var principal = await tokenService.CreatePrincipalAsync(user, request.GetScopes(), request.ClientId);
                return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }
            case OpenIddictConstants.GrantTypes.AuthorizationCode:
            case OpenIddictConstants.GrantTypes.RefreshToken:
            {
                var authenticateResult = await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
                var principal = authenticateResult?.Principal;
                if (principal is null)
                    return Forbid(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

                var user = await tokenService.ValidateUserAsync(principal);
                if (user is null)
                    return Forbid(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

                var newPrincipal = await tokenService.CreatePrincipalAsync(user, principal.GetScopes(), request.ClientId);
                return SignIn(newPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }
        }

        throw new InvalidOperationException("The specified grant type is not supported.");
    }
}
