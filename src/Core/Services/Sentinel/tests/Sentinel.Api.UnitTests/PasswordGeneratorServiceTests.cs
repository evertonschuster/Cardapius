using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Sentinel.Api.Services;

namespace Sentinel.Api.UnitTests;

public class PasswordGeneratorServiceTests
{
    [Fact]
    public void Generate_RespectsPasswordOptions()
    {
        var options = Options.Create(new PasswordOptions
        {
            RequiredLength = 10,
            RequireDigit = true,
            RequireLowercase = true,
            RequireUppercase = true,
            RequireNonAlphanumeric = true,
            RequiredUniqueChars = 4
        });

        var service = new PasswordGeneratorService(options);
        var password = service.Generate();

        password.Length.Should().BeGreaterThanOrEqualTo(10);
        password.Any(char.IsUpper).Should().BeTrue();
        password.Any(char.IsLower).Should().BeTrue();
        password.Any(char.IsDigit).Should().BeTrue();
        password.Any(ch => "!@#$%^&*()-_=+[]{};:,.<>?".Contains(ch)).Should().BeTrue();
        password.Distinct().Count().Should().BeGreaterThanOrEqualTo(4);
    }

    [Fact]
    public void Generate_UsesMinLengthAndAllowedSymbols()
    {
        var options = Options.Create(new PasswordOptions
        {
            RequiredLength = 6,
            RequireDigit = false,
            RequireLowercase = false,
            RequireUppercase = false,
            RequireNonAlphanumeric = true
        });

        var service = new PasswordGeneratorService(options);
        var password = service.Generate(minLength: 20, allowedSymbols: "*");

        password.Length.Should().Be(20);
        password.All(ch => ch == '*').Should().BeTrue();
    }
}