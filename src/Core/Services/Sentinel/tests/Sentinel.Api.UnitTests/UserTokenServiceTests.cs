using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using OpenIddict.Abstractions;
using Sentinel.Api.Models;
using Sentinel.Api.Services;
using System.Security.Claims;

namespace Sentinel.Api.UnitTests;

public class UserTokenServiceTests
{
    private static (UserTokenService Service, Mock<UserManager<ApplicationUser>> UserManager, Mock<SignInManager<ApplicationUser>> SignInManager) CreateService()
    {
        var userStore = new Mock<IUserStore<ApplicationUser>>();
        var userManager = new Mock<UserManager<ApplicationUser>>(userStore.Object, null, null, null, null, null, null, null, null);
        var contextAccessor = new Mock<IHttpContextAccessor>();
        var claimsFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
        var signInManager = new Mock<SignInManager<ApplicationUser>>(userManager.Object, contextAccessor.Object, claimsFactory.Object, Options.Create(new IdentityOptions()), null, null, null);
        var options = Options.Create(new IdentityOptions());
        var service = new UserTokenService(signInManager.Object, userManager.Object, options);
        return (service, userManager, signInManager);
    }

    [Fact]
    public async Task ValidateUserAsync_ReturnsUser_WhenCredentialsValidAndUserActive()
    {
        var (service, userManager, signInManager) = CreateService();
        var user = new ApplicationUser { UserName = "user", IsActive = true, AccessGrantedUntil = DateTime.UtcNow.AddDays(1) };

        userManager.Setup(x => x.FindByNameAsync("user")).ReturnsAsync(user);
        signInManager.Setup(x => x.CanSignInAsync(user)).ReturnsAsync(true);
        signInManager.Setup(x => x.CheckPasswordSignInAsync(user, "pass", true)).ReturnsAsync(SignInResult.Success);

        var result = await service.ValidateUserAsync("user", "pass");

        result.Should().Be(user);
    }

    [Fact]
    public async Task ValidateUserAsync_ReturnsNull_WhenUserInactive()
    {
        var (service, userManager, signInManager) = CreateService();
        var user = new ApplicationUser { UserName = "user", IsActive = false };

        userManager.Setup(x => x.FindByNameAsync("user")).ReturnsAsync(user);
        signInManager.Setup(x => x.CanSignInAsync(user)).ReturnsAsync(true);

        var result = await service.ValidateUserAsync("user", "pass");

        result.Should().BeNull();
        signInManager.Verify(x => x.CheckPasswordSignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), true), Times.Never);
    }

    [Fact]
    public async Task SignOutAsync_InvokesSignInManager()
    {
        var (service, _, signInManager) = CreateService();
        signInManager.Setup(x => x.SignOutAsync()).Returns(Task.CompletedTask).Verifiable();

        await service.SignOutAsync();

        signInManager.Verify(x => x.SignOutAsync(), Times.Once);
    }

    [Fact]
    public async Task CreatePrincipalAsync_SetsSubjectAndScopes()
    {
        var (service, _, signInManager) = CreateService();
        var user = new ApplicationUser
        {
            Id = "123",
            UserName = "admin",
            Email = "admin@gmail.com"
        };
        var principal = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim("type", "value") }));
        signInManager.Setup(x => x.CreateUserPrincipalAsync(user)).ReturnsAsync(principal);

        var result = await service.CreatePrincipalAsync(user, new[] { "email", "profile" }, "client");

        result.Should().BeSameAs(principal);
        result.FindFirst(OpenIddictConstants.Claims.Subject)!.Value.Should().Be("123");
        result.GetScopes().Should().BeEquivalentTo(new[] { "email", "profile" });
    }
}