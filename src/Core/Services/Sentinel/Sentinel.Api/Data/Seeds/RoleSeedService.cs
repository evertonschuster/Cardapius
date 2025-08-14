
using Microsoft.AspNetCore.Identity;

namespace Sentinel.Api.Data.Seeds
{
    public class RoleSeedService(RoleManager<IdentityRole> manager, IConfiguration configuration) : ISeedService
    {
        public async Task SeedAsync()
        {
            var rolesSection = configuration.GetSection("Roles");
            foreach (var serviceRoles in rolesSection.GetChildren())
            {
                foreach (var role in serviceRoles.Get<string[]>() ?? [])
                {
                    if (!await manager.RoleExistsAsync(role))
                        await manager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
