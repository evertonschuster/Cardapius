using Microsoft.AspNetCore.Identity;
using Sentinel.Api.Data;
using Sentinel.Api.Models;

namespace Sentinel.Api.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<SentinelDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
