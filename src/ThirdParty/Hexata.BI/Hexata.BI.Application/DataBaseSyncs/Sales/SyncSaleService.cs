using Hexata.BI.Application.DataBaseSyncs.Sales.Models;
using Hexata.BI.Application.Dtos;
using Hexata.BI.Application.Observabilities;
using Hexata.BI.Application.Repositories;
using Hexata.BI.Application.Services.Localizations;
using Microsoft.Extensions.Logging;
namespace Hexata.BI.Application.DataBaseSyncs.Sales
{
    public class SyncSaleService(
        IErpSaleRepository erpSaleRepository,
        IBISaleRepository bISaleRepository,
        ILocalizationService localizationService,
        Instrument instrument,
        ILogger<SyncSaleService> logger
        ) : ISyncService
    {
        public async Task<Result<SyncResultDto, SyncStatus>> SyncAsync(SyncDto syncDto, CancellationToken cancellationToken)
        {
            IEnumerable<Order> erpSales;
            using (instrument.ExecuteDataBaseQuery("List ERP Sales"))
            {
                erpSales = await erpSaleRepository.ListAsync(syncDto, cancellationToken);
            }

            if (erpSales == null || !erpSales.Any())
            {
                logger.LogInformation("No sales found for sync. SyncDto: {@SyncDto}", syncDto);
                return SyncResultDto.DoneLastPage(syncDto);
            }

            var parallelOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = 10,
                CancellationToken = cancellationToken
            };
#if DEBUG
            parallelOptions.MaxDegreeOfParallelism = 1;
#endif

            Parallel.ForEach(erpSales, parallelOptions, async erpSale =>
            {
                if (erpSale.Address == null)
                {
                    return;
                }

                var localization = await localizationService.GetLocalizationAsync(erpSale.Address);
                if (localization.IsSuccess)
                {
                    erpSale.Localization = localization.Value?.Localization;
                }
                else
                {
                    logger.LogWarning("Failed to get localization for sale. Sale: {@Sale}, Error: {@Error}", erpSale, localization.Error);
                }
            });

            using (instrument.ExecuteDataBaseCommand("Save BI Sales"))
            {
                await bISaleRepository.SaveAsync(erpSales);
            }
            return SyncResultDto.DoneAndNextPage(syncDto);
        }
    }
}
