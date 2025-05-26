using Hexata.BI.Application.DataBaseSyncs.Sales.Models;

namespace Hexata.BI.Application.Repositories
{
    public interface IBISaleRepository
    {
        Task SaveAsync(IEnumerable<Order> orders);
    }
}
