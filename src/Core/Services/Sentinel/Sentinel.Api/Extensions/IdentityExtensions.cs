using Microsoft.AspNetCore.Identity;
using Sentinel.Api.Data;

namespace Sentinel.Api.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<SentinelDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
