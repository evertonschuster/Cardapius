using Hangfire;
using Hangfire.MemoryStorage;

namespace Hexata.Worker.Extensions
{
    public static class HangFireExtension
    {
        public static IHostApplicationBuilder AddHangFire(this IHostApplicationBuilder builder)
        {
            builder.Services.AddHangfire(config => config.UseMemoryStorage());
            builder.Services.AddHangfireServer();

            return builder;
        }
    }
}
