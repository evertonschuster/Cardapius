using Hexata.BI.Application.DataBaseSyncs.Customers.Models;
using Hexata.BI.Application.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Hexata.Infrastructure.Mongo
{
    internal class BICustomerRepository(IOptions<MongoDbSettings> optionSettings) : Repository<Customer>(optionSettings, "Customers"), IBICustomerRepository
    {
        public async Task SaveAsync(IEnumerable<Customer> erpCustomers)
        {
            var requests = new List<WriteModel<Customer>>();
            foreach (var document in erpCustomers)
            {
                var filter = new FilterDefinitionBuilder<Customer>().Eq(doc => doc.Id, document.Id);
                var update = document;
                var request = new ReplaceOneModel<Customer>(filter, update)
                {
                    IsUpsert = true,
                };
                requests.Add(request);
            }


            await _collection.BulkWriteAsync(requests);
        }
    }
}
