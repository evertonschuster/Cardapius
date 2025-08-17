using System;

namespace Sentinel.Api.Models;

public class TokenLifetimeOption
{
    public int Id { get; set; }

    public string ClientId { get; set; } = string.Empty;

    public string AllowedScopes { get; set; } = string.Empty;

    public int AccessTokenLifetimeMinutes { get; set; }

    public int RefreshTokenLifetimeMinutes { get; set; }
}

public readonly record struct TokenLifetime(TimeSpan AccessToken, TimeSpan RefreshToken);
