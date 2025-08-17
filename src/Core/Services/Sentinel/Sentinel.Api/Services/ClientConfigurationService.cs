using Microsoft.Extensions.Configuration;
using OpenIddict.Abstractions;

namespace Sentinel.Api.Services;

public record TokenLifetimeOptions(TimeSpan? AccessToken, TimeSpan? RefreshToken, TimeSpan? AuthorizationCode);

public interface IClientConfigurationService
{
    string[] GetAllowedScopes(string? clientId);
    TokenLifetimeOptions GetTokenLifetimes(string? clientId);
}

public class ClientConfigurationService(IConfiguration configuration) : IClientConfigurationService
{
    private static readonly string[] DefaultAllowedScopes =
    {
        OpenIddictConstants.Scopes.Email,
        OpenIddictConstants.Scopes.Profile,
        OpenIddictConstants.Scopes.OpenId,
        OpenIddictConstants.Scopes.OfflineAccess,
        "api"
    };

    public string[] GetAllowedScopes(string? clientId)
    {
        if (string.IsNullOrEmpty(clientId))
            return DefaultAllowedScopes;

        var scopes = configuration.GetSection($"Clients:{clientId}:AllowedScopes").Get<string[]>() ?? Array.Empty<string>();
        return scopes.Length > 0 ? scopes : DefaultAllowedScopes;
    }

    public TokenLifetimeOptions GetTokenLifetimes(string? clientId)
    {
        var defaults = configuration.GetSection("OpenIddict:TokenLifetimes");
        var accessDefault = defaults.GetValue<int?>("AccessToken");
        var refreshDefault = defaults.GetValue<int?>("RefreshToken");
        var codeDefault = defaults.GetValue<int?>("AuthorizationCode");

        var section = string.IsNullOrEmpty(clientId)
            ? null
            : configuration.GetSection($"Clients:{clientId}:TokenLifetimes");

        var access = section?.GetValue<int?>("AccessToken") ?? accessDefault;
        var refresh = section?.GetValue<int?>("RefreshToken") ?? refreshDefault;
        var code = section?.GetValue<int?>("AuthorizationCode") ?? codeDefault;

        return new TokenLifetimeOptions(ToMinutes(access), ToMinutes(refresh), ToMinutes(code));
    }

    private static TimeSpan? ToMinutes(int? value) => value.HasValue ? TimeSpan.FromMinutes(value.Value) : null;
}
