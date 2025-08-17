using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using Sentinel.Api.Data;
using Sentinel.Api.Models;

namespace Sentinel.Api.Services;

public class TokenLifetimeOptions
{
    private readonly SentinelDbContext _context;
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

    public TokenLifetimeOptions(SentinelDbContext context)
    {
        _context = context;
    }

    public async Task<string[]> GetAllowedScopesAsync(string clientId)
    {
        var option = await _context.TokenLifetimeOptions.FirstOrDefaultAsync(o => o.ClientId == clientId);
        if (option != null && !string.IsNullOrWhiteSpace(option.AllowedScopes))
        {
            try
            {
                return JsonSerializer.Deserialize<string[]>(option.AllowedScopes) ?? DefaultAllowedScopes;
            }
            catch
            {
                // ignore parsing errors
            }
        }

        if (option == null)
        {
            option = new TokenLifetimeOption
            {
                ClientId = clientId,
                AllowedScopes = JsonSerializer.Serialize(DefaultAllowedScopes),
                AccessTokenLifetimeMinutes = (int)DefaultTokenLifetime.AccessToken.TotalMinutes,
                RefreshTokenLifetimeMinutes = (int)DefaultTokenLifetime.RefreshToken.TotalMinutes
            };
            _context.TokenLifetimeOptions.Add(option);
            await _context.SaveChangesAsync();
        }
        else if (string.IsNullOrWhiteSpace(option.AllowedScopes))
        {
            option.AllowedScopes = JsonSerializer.Serialize(DefaultAllowedScopes);
            await _context.SaveChangesAsync();
        }

        return DefaultAllowedScopes;
    }

    public async Task<TokenLifetime> GetTokenLifetimesAsync(string clientId)
    {
        var option = await _context.TokenLifetimeOptions.FirstOrDefaultAsync(o => o.ClientId == clientId);
        if (option == null)
        {
            option = new TokenLifetimeOption
            {
                ClientId = clientId,
                AllowedScopes = JsonSerializer.Serialize(DefaultAllowedScopes),
                AccessTokenLifetimeMinutes = (int)DefaultTokenLifetime.AccessToken.TotalMinutes,
                RefreshTokenLifetimeMinutes = (int)DefaultTokenLifetime.RefreshToken.TotalMinutes
            };
            _context.TokenLifetimeOptions.Add(option);
            await _context.SaveChangesAsync();
            return DefaultTokenLifetime;
        }

        return new TokenLifetime(
            TimeSpan.FromMinutes(option.AccessTokenLifetimeMinutes),
            TimeSpan.FromMinutes(option.RefreshTokenLifetimeMinutes));
    }
}
