using Hexata.BI.Application.DataBaseSyncs.Customers.Models;
using Hexata.BI.Application.Repositories;
using Microsoft.EntityFrameworkCore;


namespace Hexata.Infrastructure.Postgres.Respositories
{
    internal class BICustomerRepository(HexataDbContext dbContext) : IBICustomerRepository
    {
        public async Task SaveAsync(IEnumerable<Customer> erpCustomers)
        {
            foreach (var customer in erpCustomers)
            {
                var existing = await dbContext.Customers
                    .FirstOrDefaultAsync(c => c.Id == customer.Id);

                if (existing is null)
                    dbContext.Customers.Add(customer);
                else
                    dbContext.Entry(existing).CurrentValues.SetValues(customer);
            }
            await dbContext.SaveChangesAsync();
        }
    }
}