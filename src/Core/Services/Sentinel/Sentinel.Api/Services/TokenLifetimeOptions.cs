using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text.Json;
using OpenIddict.Abstractions;
using Sentinel.Api.Models;
using Sentinel.Api.Extensions;

namespace Sentinel.Api.Services;

public class TokenLifetimeOptions
{
    private readonly IOpenIddictApplicationManager _manager;

    private static readonly string[] DefaultAllowedScopes =
    {
        OpenIddictConstants.Scopes.Email,
        OpenIddictConstants.Scopes.Profile,
        OpenIddictConstants.Scopes.OpenId,
        OpenIddictConstants.Scopes.OfflineAccess,
        "api"
    };

    private static readonly TokenLifetime DefaultTokenLifetime =
        new(TimeSpan.FromHours(1), TimeSpan.FromDays(1));

    public TokenLifetimeOptions(IOpenIddictApplicationManager manager)
    {
        _manager = manager;
    }

    public async Task<string[]> GetAllowedScopesAsync(string clientId)
    {
        var application = await _manager.FindByClientIdAsync(clientId);
        if (application is null)
            return DefaultAllowedScopes;

        var permissions = (await _manager.GetPermissionsAsync(application)).ToList();
        var prefix = OpenIddictConstants.Permissions.Prefixes.Scope;
        var allowedScopes = permissions
            .Where(p => p.StartsWith(prefix, StringComparison.Ordinal))
            .Select(p => p[prefix.Length..])
            .ToArray();

        if (allowedScopes.Length == 0)
        {
            var newPermissions = permissions
                .Where(p => !p.StartsWith(prefix, StringComparison.Ordinal))
                .Concat(DefaultAllowedScopes.Select(s => prefix + s))
                .ToList();
            await _manager.SetPermissionsAsync(application, newPermissions);
            return DefaultAllowedScopes;
        }

        return allowedScopes;
    }

    public async Task<TokenLifetime> GetTokenLifetimesAsync(string clientId)
    {
        var application = await _manager.FindByClientIdAsync(clientId);
        if (application is null)
            return DefaultTokenLifetime;

        var properties = await _manager.GetPropertiesAsync(application);
        var access = GetInt(properties, OpenIddictApplicationDescriptorExtensions.AccessTokenLifetimeProperty);
        var refresh = GetInt(properties, OpenIddictApplicationDescriptorExtensions.RefreshTokenLifetimeProperty);

        if (access.HasValue && refresh.HasValue)
            return new TokenLifetime(TimeSpan.FromMinutes(access.Value), TimeSpan.FromMinutes(refresh.Value));

        var dict = properties.ToDictionary(p => p.Key, p => p.Value);
        dict[OpenIddictApplicationDescriptorExtensions.AccessTokenLifetimeProperty] = JsonSerializer.SerializeToElement((int)DefaultTokenLifetime.AccessToken.TotalMinutes);
        dict[OpenIddictApplicationDescriptorExtensions.RefreshTokenLifetimeProperty] = JsonSerializer.SerializeToElement((int)DefaultTokenLifetime.RefreshToken.TotalMinutes);
        await _manager.SetPropertiesAsync(application, dict);
        return DefaultTokenLifetime;
    }

    private static int? GetInt(ImmutableDictionary<string, JsonElement> properties, string key)
    {
        if (properties.TryGetValue(key, out var element) && element.ValueKind == JsonValueKind.Number)
            return element.GetInt32();
        return null;
    }
}
