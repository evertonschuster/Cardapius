using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Sentinel.Api.Services;
using Xunit;

namespace Sentinel.Api.UnitTests;

public class ClientConfigurationServiceTests
{
    [Fact]
    public void GetTokenLifetimes_Returns_Client_Overrides()
    {
        var settings = new Dictionary<string, string?>
        {
            ["OpenIddict:TokenLifetimes:AccessToken"] = "60",
            ["OpenIddict:TokenLifetimes:RefreshToken"] = "120",
            ["OpenIddict:TokenLifetimes:AuthorizationCode"] = "5",
            ["Clients:console:TokenLifetimes:AccessToken"] = "30"
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(settings)
            .Build();
        var service = new ClientConfigurationService(configuration);

        var lifetimes = service.GetTokenLifetimes("console");

        Assert.Equal(TimeSpan.FromMinutes(30), lifetimes.AccessToken);
        Assert.Equal(TimeSpan.FromMinutes(120), lifetimes.RefreshToken);
        Assert.Equal(TimeSpan.FromMinutes(5), lifetimes.AuthorizationCode);
    }
}
