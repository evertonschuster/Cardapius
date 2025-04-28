using Hexata.BI.Application.DataBaseSyncs;
using Hexata.BI.Application.Repositories;

namespace Hexata.BI.Worker
{
    public class Worker(ILogger<Worker> _logger, IServiceProvider serviceProvider) : BackgroundService
    {

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var syncManager = scope.ServiceProvider.GetRequiredService<SyncManagerService>();
                    await syncManager.SyncAsync(stoppingToken);
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
