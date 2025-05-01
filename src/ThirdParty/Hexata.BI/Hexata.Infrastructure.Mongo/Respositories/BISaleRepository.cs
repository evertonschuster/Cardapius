using Hexata.BI.Application.DataBaseSyncs.Sales.Models;
using Hexata.BI.Application.Repositories;
using Hexata.Infrastructure.Mongo.Documents;
using Hexata.Infrastructure.Mongo.Respositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Hexata.Infrastructure.Mongo.Repositories
{
    internal class BISaleRepository : Repository<Order>, IBISaleRepository
    {
        private readonly IMongoCollection<OrderItem> _orderItemCollection;
        private readonly IMongoCollection<OrderItemAuxiliarySpecie> _orderItemAuxiliarySpeciesCollection;

        public BISaleRepository(IOptions<MongoDbSettings> optionSettings)
            : base(optionSettings, "Saidas")
        {
            _orderItemCollection = _database.GetCollection<OrderItem>("Saida Items");
            _orderItemAuxiliarySpeciesCollection = _database.GetCollection<OrderItemAuxiliarySpecie>("Saida Item Especie Auxiliares");
        }

        public async Task SaveAsync(IEnumerable<Order> orders)
        {
            await UpsertOrdersAsync(orders);

            foreach (var order in orders)
                await SyncOrderItemsAsync(order);
        }

        private async Task UpsertOrdersAsync(IEnumerable<Order> orders)
        {
            var requests = orders.Select(order =>
                new ReplaceOneModel<Order>(Builders<Order>.Filter.Eq(o => o.Id, order.Id), order)
                { IsUpsert = true });

            if (requests.Any())
                await _collection.BulkWriteAsync(requests);
        }

        private async Task SyncOrderItemsAsync(Order order)
        {
            var newItems = order.Items ?? [];

            var existingItemIds = await GetExistingItemIdsAsync(order.Id);
            var newItemIds = newItems.Select(item => item.Id).ToHashSet();

            await RemoveDeletedItemsAsync(existingItemIds, newItemIds);
            await UpsertOrderItemsAsync(newItems);
        }

        private async Task<HashSet<int>> GetExistingItemIdsAsync(int orderId) =>
            (await _orderItemCollection.Find(i => i.OrderId == orderId)
                                       .Project(i => i.Id)
                                       .ToListAsync()).ToHashSet();

        private async Task RemoveDeletedItemsAsync(HashSet<int> existingItemIds, HashSet<int> newItemIds)
        {
            var itemsToRemove = existingItemIds.Except(newItemIds).ToList();

            if (!itemsToRemove.Any()) return;

            var itemFilter = Builders<OrderItem>.Filter.In(item => item.Id, itemsToRemove);
            var auxSpeciesFilter = Builders<OrderItemAuxiliarySpecie>.Filter.In(specie => specie.OrderItemId, itemsToRemove);

            await _orderItemAuxiliarySpeciesCollection.DeleteManyAsync(auxSpeciesFilter);
            await _orderItemCollection.DeleteManyAsync(itemFilter);
        }

        private async Task UpsertOrderItemsAsync(IEnumerable<OrderItem> items)
        {
            if (!items.Any()) return;

            var requests = items.Select(item => new ReplaceOneModel<OrderItem>(
                Builders<OrderItem>.Filter.Eq(i => i.Id, item.Id), item)
            { IsUpsert = true });

            await _orderItemCollection.BulkWriteAsync(requests);

            await SyncAuxiliarySpeciesAsync(items);
        }

        private async Task SyncAuxiliarySpeciesAsync(IEnumerable<OrderItem> items)
        {
            var itemAuxSpecies = items.SelectMany(item => item.AuxiliarySpecies ?? [],
                (item, auxSpecie) => new OrderItemAuxiliarySpecie(item.OrderId, item.Id, auxSpecie)).ToHashSet();

            var orderId = items.First().OrderId;
            var existingAuxSpecies = (await _orderItemAuxiliarySpeciesCollection
                .Find(specie => specie.OrderId == orderId)
                .ToListAsync()).ToHashSet();

            var speciesToRemove = existingAuxSpecies.Except(itemAuxSpecies).Select(e => e.Id).ToList();
            var speciesToAddOrUpdate = itemAuxSpecies.Except(existingAuxSpecies);

            if (speciesToRemove.Any())
                await _orderItemAuxiliarySpeciesCollection.DeleteManyAsync(
                    Builders<OrderItemAuxiliarySpecie>.Filter.In(specie => specie.Id, speciesToRemove));

            if (speciesToAddOrUpdate.Any())
            {
                var requestsAuxSpecies = speciesToAddOrUpdate.Select(specie =>
                    new ReplaceOneModel<OrderItemAuxiliarySpecie>(
                        Builders<OrderItemAuxiliarySpecie>.Filter.Eq(s => s.Id, specie.Id), specie)
                    { IsUpsert = true });

                await _orderItemAuxiliarySpeciesCollection.BulkWriteAsync(requestsAuxSpecies);
            }
        }
    }
}
