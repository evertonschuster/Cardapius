using Hangfire;
using Hangfire.MemoryStorage;
using Hexata.BI.Application.Workflows.SendOrderBI;

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

        public static IHost UseHangFire(this IHost host)
        {
            var sendOrderBIJob = host.Services.GetService<SendOrderBIJob>();
            var backgroundJobClient = host.Services.GetService<IBackgroundJobClient>()!;
            var recurringJobManager = host.Services.GetService<IRecurringJobManager>()!;



            backgroundJobClient.Enqueue(() => Console.WriteLine("Agendando Workflow via Hangfire..."));
            backgroundJobClient.Enqueue(() => sendOrderBIJob!.ExecutarWorkflowAsync());
            recurringJobManager.AddOrUpdate("sendOrderBI-job", () => sendOrderBIJob!.ExecutarWorkflowAsync(), "0 */12 * * *");

            return host;
        }
    }
}
