using BuildingBlock.Application;
using BuildingBlock.Application.Services;

namespace BuildingBlock.Infra.DataBase.EntityFramework
{
    internal class AppUnitOfWork(
        IDbContext dbContext,
        IDomainEventService domainEventService

        ) : IUnitOfWork
    {
        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            using var contextTransaction = await dbContext.GetTransactionAsync(cancellationToken);

            try
            {
                var changedRoot = dbContext.GetChangeRoot();
                var events = domainEventService.GetDomainOutboxEvents(changedRoot);

                int? firstSave = null;

                if (events.Count > 0)
                {
                    await domainEventService.StoreDomainEventsAsync(events);
                    firstSave = await dbContext.SaveChangesAsync(cancellationToken);
                    await domainEventService.EmitEventsAsync(events);
                }

                var secoundSave = await dbContext.SaveChangesAsync(cancellationToken);
                await contextTransaction.CommitAsync(cancellationToken);

                return firstSave ?? secoundSave;
            }
            catch
            {
                await contextTransaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}