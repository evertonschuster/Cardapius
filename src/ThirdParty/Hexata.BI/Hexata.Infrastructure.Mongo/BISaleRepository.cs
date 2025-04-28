using Hexata.BI.Application.DataBaseSyncs.Sales.Models;
using Hexata.BI.Application.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Hexata.Infrastructure.Mongo
{
    internal class BISaleRepository(IOptions<MongoDbSettings> optionSettings) : Repository<Order>(optionSettings, "Orders"), IBISaleRepository
    {
        public async Task SaveAsync(IEnumerable<Order> erpSales)
        {
            var requests = new List<WriteModel<Order>>();
            foreach (var document in erpSales)
            {
                var filter = new FilterDefinitionBuilder<Order>().Eq(doc => doc.Id, document.Id);
                var update = document;
                var request = new ReplaceOneModel<Order>(filter, update)
                {
                    IsUpsert = true,
                };
                requests.Add(request);
            }


            await _collection.BulkWriteAsync(requests);

            var itemCollection = _database.GetCollection<OrderItem>("OrderItems");
            var items = erpSales.SelectMany(e => e.Items ?? []);
            var requestItems = new List<WriteModel<OrderItem>>();
            foreach (var document in items)
            {
                var filter = new FilterDefinitionBuilder<OrderItem>().Eq(doc => doc.Id, document.Id);
                var update = document;
                var request = new ReplaceOneModel<OrderItem>(filter, update)
                {
                    IsUpsert = true,
                };
                requestItems.Add(request);
            }
            await itemCollection.BulkWriteAsync(requestItems);
        }
    }
}
