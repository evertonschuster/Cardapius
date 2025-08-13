
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sentinel.Api.Models;
using Sentinel.Api.Services;

namespace Sentinel.Api.Data.Seeds
{
    public class UserSeedService(
        UserManager<ApplicationUser> manager,
        RoleManager<IdentityRole> roleManager,
        PasswordGeneratorService passwordGenerator,
        IHostEnvironment host,
        ILogger<UserSeedService> logger
        ) : ISeedService
    {
        private const string adminEmail = "admin@local.com";
        private const string adminPassword = "12345678";

        public async Task SeedAsync()
        {
            var admin = await manager.FindByEmailAsync(adminEmail);
            var backupPasswordValidators = manager.PasswordValidators.ToList();

            if (admin is null)
            {
                try
                {
                    admin = new ApplicationUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true,
                        LockoutEnd = DateTime.UtcNow,
                        IsActive = true,
                        AccessGrantedUntil = DateTime.UtcNow.AddYears(1)
                    };

                    if (!host.IsProduction())
                    {
                        manager.PasswordValidators.Clear();
                    }

                    var password = host.IsProduction() ? passwordGenerator.Generate() : adminPassword;
                    var createResult = await manager.CreateAsync(admin, password);
                    if (!createResult.Succeeded)
                        throw new Exception(string.Join("; ", createResult.Errors.Select(e => e.Description)));

                    var allRoleNames = await roleManager.Roles.Select(r => r.Name!).ToListAsync();
                    if (allRoleNames.Count > 0)
                    {
                        await manager.AddToRolesAsync(admin, allRoleNames);
                    }

                    logger.LogInformation("Admin user created with email: {Email} and password: {Password}", adminEmail, password);
                }
                finally
                {
                    if (!host.IsProduction())
                    {
                        manager.PasswordValidators.Clear();
                        foreach (var v in backupPasswordValidators) manager.PasswordValidators.Add(v);
                    }
                }
            }
        }
    }
}
