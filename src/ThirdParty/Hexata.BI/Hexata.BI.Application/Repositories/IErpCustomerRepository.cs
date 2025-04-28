using Hexata.BI.Application.DataBaseSyncs;
using Hexata.BI.Application.DataBaseSyncs.Customers.Models;

namespace Hexata.BI.Application.Repositories
{
    public interface IErpCustomerRepository
    {
        Task<IEnumerable<Customer>> ListAsync(SyncDto syncDto, CancellationToken cancellationToken);
    }
}
