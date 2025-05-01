using Hexata.BI.Application.DataBaseSyncs.Customers.Models;
using Hexata.BI.Application.Dtos;
using Hexata.BI.Application.Observabilities;
using Hexata.BI.Application.Repositories;
using Microsoft.Extensions.Logging;

namespace Hexata.BI.Application.DataBaseSyncs.Customers
{
    internal class SyncCustomerService(
        IErpCustomerRepository erpCustomerRepository,
        IBICustomerRepository bICustomerRepository,
        Instrument instrument,
        ILogger<SyncCustomerService> logger) : ISyncService
    {
        public async Task<Result<SyncResultDto, SyncStatus>> SyncAsync(SyncDto syncDto, CancellationToken cancellationToken)
        {
            IEnumerable<Customer> erpCustomers;
            using (instrument.ExecuteDataBaseQuery("List ERP Customer"))
            {
                erpCustomers = await erpCustomerRepository.ListAsync(syncDto, cancellationToken);
            }

            if (erpCustomers == null || !erpCustomers.Any())
            {
                logger.LogInformation("No Customer found for sync. SyncDto: {@SyncDto}", syncDto);
                return SyncResultDto.DoneLastPage(syncDto);
            }

            using (instrument.ExecuteDataBaseCommand("Save BI Customer"))
            {
                await bICustomerRepository.SaveAsync(erpCustomers);
            }

            return SyncResultDto.DonetPage(syncDto);
        }
    }
}
