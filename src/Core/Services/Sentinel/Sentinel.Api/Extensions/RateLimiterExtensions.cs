using System.Threading.RateLimiting;

namespace Sentinel.Api.Extensions
{
    public static class RateLimiterExtensions
    {
        public static IServiceCollection AddAppRateLimiter(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                {
                    if (context.Request.Path.StartsWithSegments("/connect/token"))
                    {
                        return RateLimitPartition.GetFixedWindowLimiter("token", _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 10,
                            Window = TimeSpan.FromMinutes(1),
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                            QueueLimit = 2
                        });
                    }

                    return RateLimitPartition.GetNoLimiter("none");
                });
            });

            return services;
        }

        public static IApplicationBuilder UseAppRateLimiter(this IApplicationBuilder app)
        {
            app.UseRateLimiter();
            return app;
        }
    }
}
