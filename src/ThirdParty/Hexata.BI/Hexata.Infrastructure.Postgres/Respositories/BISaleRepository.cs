using Hexata.BI.Application.DataBaseSyncs.Sales.Models;
using Hexata.BI.Application.Repositories;
using Hexata.Infrastructure.Postgres.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hexata.Infrastructure.Postgres.Respositories
{
    internal class BISaleRepository(HexataDbContext dbContext) : IBISaleRepository
    {
        public async Task SaveAsync(IEnumerable<Order> orders)
        {
            foreach (var order in orders)
            {
                // Upsert da Order
                var existingOrder = await dbContext.Orders
                    .Include(o => o.Items)
                    .FirstOrDefaultAsync(o => o.Id == order.Id);

                if (existingOrder is null)
                {
                    dbContext.Orders.Add(order);
                }
                else
                {
                    dbContext.Entry(existingOrder).CurrentValues.SetValues(order);

                    // Sincroniza os itens da ordem
                    await SyncOrderItemsAsync(existingOrder, order.Items ?? []);
                }
            }

            await dbContext.SaveChangesAsync();
        }

        private async Task SyncOrderItemsAsync(Order existingOrder, List<OrderItem> newItems)
        {
            // Carrega os itens existentes
            var existingItems = existingOrder.Items?.ToList() ?? [];

            // Remove itens que não existem mais
            var newItemIds = newItems.Select(i => i.Id).ToHashSet();
            var itemsToRemove = existingItems.Where(i => !newItemIds.Contains(i.Id)).ToList();
            foreach (var item in itemsToRemove)
            {
                dbContext.OrderItems.Remove(item);
            }

            // Upsert dos itens
            foreach (var newItem in newItems)
            {
                var existingItem = existingItems.FirstOrDefault(i => i.Id == newItem.Id);
                if (existingItem is null)
                {
                    // Garante o relacionamento
                    newItem.OrderId = existingOrder.Id;
                    dbContext.OrderItems.Add(newItem);
                }
                else
                {
                    dbContext.Entry(existingItem).CurrentValues.SetValues(newItem);
                    await SyncAuxiliarySpeciesAsync(existingItem, newItem.AuxiliarySpecies ?? []);
                }
            }
        }

        private async Task SyncAuxiliarySpeciesAsync(OrderItem existingItem, List<string> newAuxSpecies)
        {
            // Carrega espécies auxiliares existentes
            var existingAuxSpecies = await dbContext.OrderItemAuxiliarySpecies
                .Where(s => s.OrderItemId == existingItem.Id)
                .ToListAsync();

            var newAuxSpeciesSet = newAuxSpecies.ToHashSet();

            // Remove espécies auxiliares que não existem mais
            var toRemove = existingAuxSpecies.Where(s => !newAuxSpeciesSet.Contains(s.Id)).ToList();
            dbContext.OrderItemAuxiliarySpecies.RemoveRange(toRemove);

            // Adiciona ou atualiza espécies auxiliares
            foreach (var auxSpecie in newAuxSpecies)
            {
                var existing = existingAuxSpecies.FirstOrDefault(s => s.Id == auxSpecie);
                if (existing is null)
                {
                    dbContext.OrderItemAuxiliarySpecies.Add(
                        new OrderItemAuxiliarySpecie(existingItem.OrderId, existingItem.Id, auxSpecie)
                    );
                }
                // Se precisar atualizar propriedades, faça aqui
            }
        }
    }
}