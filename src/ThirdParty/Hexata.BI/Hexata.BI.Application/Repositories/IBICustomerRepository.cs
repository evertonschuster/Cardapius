using Hexata.BI.Application.DataBaseSyncs.Customers.Models;

namespace Hexata.BI.Application.Repositories
{
    public interface IBICustomerRepository
    {
        Task SaveAsync(IEnumerable<Customer> erpCustomers);
    }
}
