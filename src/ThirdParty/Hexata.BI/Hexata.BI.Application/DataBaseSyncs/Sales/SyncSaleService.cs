﻿using Hexata.BI.Application.DataBaseSyncs.Sales.Models;
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
                logger.LogInformation("Carregandos dados do banco de dados ERP");
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

            logger.LogInformation("Processando Geo localização das vendas");
            await Parallel.ForEachAsync(erpSales, parallelOptions, async (erpSale, a) =>
             {
                 if (erpSale.Address == null)
                 {
                     return;
                 }

                 var localization = await localizationService.GetLocalizationAsync(erpSale.Address);
                 if (localization.IsSuccess)
                 {
                     erpSale.Localization = localization.Value;
                 }
                 else
                 {
                     logger.LogWarning("Failed to get localization for sale. Sale: {@Sale}, Error: {@Error}", erpSale, localization.Error);
                 }
             });

            using (instrument.ExecuteDataBaseCommand("Save BI Sales"))
            {
                logger.LogInformation("Enviando dados para o banco de dados do BI");
                await bISaleRepository.SaveAsync(erpSales);
            }

            logger.LogInformation("Sincronização concluída com sucesso. SyncDto: {@SyncDto}", syncDto);
            return SyncResultDto.DonetPage(syncDto);
        }
    }
}
