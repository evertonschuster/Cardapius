using Hexata.BI.Application.Repositories;
using Microsoft.Extensions.Logging;

namespace Hexata.BI.Application.DataBaseSyncs
{
    public class SyncManagerService(
        IEnumerable<ISyncService> syncServices,
        IServiceStateRepository stateRepository,
        ILogger<SyncManagerService> logger)
    {
        public async Task SyncAsync(CancellationToken cancellationToken)
        {
            foreach (var service in syncServices)
            {
                var serviceName = service.GetType().FullName ?? service.GetType().Name;
                var currentState = await LoadStateAsync(serviceName, cancellationToken);

                bool hasMorePages;

                try
                {

                    do
                    {
                        logger.LogInformation(
                            "Starting sync for '{ServiceName}' - Page: {Page}, Reference: {Reference}",
                            serviceName,
                            currentState.Page,
                            currentState.Reference);

                        var result = await service.SyncAsync(currentState, cancellationToken);

                        if (result.IsSuccess)
                        {
                            var syncResult = result.Value!;
                            hasMorePages = !syncResult.IsLastPage;
                            currentState = hasMorePages ? syncResult.ToSyncDto() : SyncDto.Create();

                            await stateRepository.SaveStateAsync(serviceName, currentState, cancellationToken);

                            if (!hasMorePages)
                            {
                                logger.LogInformation("Sync '{ServiceName}' completed successfully", serviceName);
                            }
                        }
                        else
                        {
                            logger.LogCritical(
                                "Sync '{ServiceName}' failed with error: {Error}",
                                serviceName,
                                result.Error);

                            await stateRepository.SaveErrorAsync(serviceName, result.Error, cancellationToken);
                            break;
                        }

                    } while (hasMorePages);
                }
                catch (Exception ex)
                {
                    logger.LogError(
                        ex,
                        "An error occurred during sync for '{ServiceName}': {Message}",
                        serviceName,
                        ex.Message);
                    await stateRepository.SaveErrorAsync(serviceName, ex, cancellationToken);
                }
            }
        }

        private async Task<SyncDto> LoadStateAsync(string serviceName, CancellationToken cancellationToken)
        {
            var state = await stateRepository.GetStateAsync(serviceName, cancellationToken);
            return state?.State ?? SyncDto.Create();
        }
    }
}
