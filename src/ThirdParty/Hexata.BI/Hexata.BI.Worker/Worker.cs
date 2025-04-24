using Hexata.BI.Application.Repositories;

namespace Hexata.BI.Worker
{
    public class Worker(ILogger<Worker> _logger, ILocalizationRepository localizationRepository) : BackgroundService
    {

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //await localizationRepository.CleanDataAsync();
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
