using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Polly;
using System.Net;
using System.Threading.RateLimiting;

namespace Sentinel.Api.Extensions
{
    public static class RateLimiterExtensions
    {
        public static IServiceCollection AddAppRateLimiter(this IServiceCollection services, IConfiguration configuration)
        {
            var whitelist = configuration.GetSection("RateLimiting:IpWhitelist").Get<string[]>() ?? [];

            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                options.OnRejected = (context, token) =>
                {
                    context.HttpContext.Response.Headers["Retry-After"] = "60";
                    return ValueTask.CompletedTask;
                };

                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                {
                    var ip = GetClientIp(httpContext);

                    if (ip != null && whitelist.Contains(ip.ToString()))
                        return RateLimitPartition.GetNoLimiter($"wl:{ip}");

                    var key = $"ip:{ip}";
                    return RateLimitPartition.GetTokenBucketLimiter(key, _ => new TokenBucketRateLimiterOptions
                    {
                        TokenLimit = 240,
                        TokensPerPeriod = 120,
                        ReplenishmentPeriod = TimeSpan.FromMinutes(1),
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 50,
                        AutoReplenishment = true
                    });
                });
            });

            return services;
        }

        public static IApplicationBuilder UseAppRateLimiter(this IApplicationBuilder app)
        {
            app.UseRateLimiter();
            return app;
        }

        static IPAddress? GetClientIp(HttpContext ctx)
        {
            var xff = ctx.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(xff))
            {
                var first = xff.Split(',')[0].Trim();
                if (IPAddress.TryParse(first, out var ipFromHeader))
                    return ipFromHeader;
            }
            return ctx.Connection.RemoteIpAddress;
        }
    }
}
