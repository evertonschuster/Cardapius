using System;
namespace Sentinel.Api.Models;

public readonly record struct TokenLifetime(TimeSpan AccessToken, TimeSpan RefreshToken);
