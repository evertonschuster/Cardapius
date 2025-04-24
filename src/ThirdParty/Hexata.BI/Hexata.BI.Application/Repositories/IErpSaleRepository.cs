using Hexata.BI.Application.DataBaseSyncs;
using Hexata.BI.Application.DataBaseSyncs.Sales.Models;

namespace Hexata.BI.Application.Repositories
{
    public interface IErpSaleRepository
    {
        Task<IEnumerable<Order>> ListAsync(SyncDto syncDto, CancellationToken cancellationToken);
    }
}
