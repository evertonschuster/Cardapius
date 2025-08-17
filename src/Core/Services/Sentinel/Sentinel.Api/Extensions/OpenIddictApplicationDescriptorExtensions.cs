using System;
using System.Text.Json;
using OpenIddict.Abstractions;
using Sentinel.Api.Models;

namespace Sentinel.Api.Extensions;

public static class OpenIddictApplicationDescriptorExtensions
{
    public const string AccessTokenLifetimeProperty = "AccessTokenLifetimeMinutes";
    public const string RefreshTokenLifetimeProperty = "RefreshTokenLifetimeMinutes";

    public static void SetTokenLifetimes(this OpenIddictApplicationDescriptor descriptor, TokenLifetime lifetime)
    {
        descriptor.Properties[AccessTokenLifetimeProperty] = JsonSerializer.SerializeToElement((int)lifetime.AccessToken.TotalMinutes);
        descriptor.Properties[RefreshTokenLifetimeProperty] = JsonSerializer.SerializeToElement((int)lifetime.RefreshToken.TotalMinutes);
    }

    public static TokenLifetime GetTokenLifetimes(this OpenIddictApplicationDescriptor descriptor, TokenLifetime @default)
    {
        if (descriptor.Properties.TryGetValue(AccessTokenLifetimeProperty, out var accessElement) &&
            accessElement.ValueKind == JsonValueKind.Number &&
            descriptor.Properties.TryGetValue(RefreshTokenLifetimeProperty, out var refreshElement) &&
            refreshElement.ValueKind == JsonValueKind.Number)
        {
            return new TokenLifetime(TimeSpan.FromMinutes(accessElement.GetInt32()),
                                     TimeSpan.FromMinutes(refreshElement.GetInt32()));
        }
        return @default;
    }
}
