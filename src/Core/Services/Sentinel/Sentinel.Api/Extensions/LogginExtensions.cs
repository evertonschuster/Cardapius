using Serilog;

namespace Sentinel.Api.Extensions
{
    public static class LogginExtensions
    {
        public static IHostBuilder AddAppLogging(this IHostBuilder host, IConfiguration configuration)
        {
            host.UseSerilog((ctx, cfg) =>
             {
                 cfg.ReadFrom.Configuration(ctx.Configuration);
             });

            return host;
        }

        public static IApplicationBuilder UseAppLogging(this IApplicationBuilder app)
        {
            app.UseSerilogRequestLogging();

            return app;
        }
    }
}
