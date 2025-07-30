using BuildingBlock.Application.Entities;
using BuildingBlock.Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace BuildingBlock.Infra.DataBase.EntityFramework.Interceptors
{
    public class EmitDomainEventInterceptor(IDomainEventService domainEventService, ILogger<EmitDomainEventInterceptor> logger) : SaveChangesInterceptor
    {
        private readonly List<OutboxMessageEntity> _pendingOutboxMessages = [];

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            logger.LogDebug("Intercept SavingChanges");

            var context = eventData.Context;
            if (context is not null)
            {
                CollectDomainEvents(context);
            }

            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            logger.LogDebug("Intercept SavingChangesAsync");

            var context = eventData.Context;
            if (context is not null)
            {
                CollectDomainEvents(context);
            }

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            if (_pendingOutboxMessages.Count == 0)
            {
                return result;
            }

            logger.LogDebug("Intercept SavedChanges, publishing {Count} events", _pendingOutboxMessages.Count);
            PublishPendingEvents(eventData.Context);

            return base.SavedChanges(eventData, result);
        }

        public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            if (_pendingOutboxMessages.Count == 0)
                return result;

            logger.LogDebug("Intercept SavedChangesAsync, publishing {Count} events", _pendingOutboxMessages.Count);
            await PublishPendingEventsAsync(eventData.Context, cancellationToken);

            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        private void CollectDomainEvents(DbContext context)
        {
            var roots = context.ChangeTracker
                .Entries<IAggregateRoot>()
                .Select(e => e.Entity)
                .ToList();

            if (roots.Count == 0)
            {
                return;
            }

            var events = domainEventService.GetDomainOutboxEvents(roots);

            if (events == null || events.Count == 0)
            {
                logger.LogInformation("No domain events to collect");
                return;
            }

            context.AddRange(events);
            _pendingOutboxMessages.AddRange(events);

            foreach (var root in roots)
            {
                root.ClearDomainEvents();
            }

            logger.LogInformation("Collected {Count} domain events", events.Count);
        }

        private void PublishPendingEvents(DbContext? context)
        {
            domainEventService.EmitEvents(_pendingOutboxMessages);
            _pendingOutboxMessages.Clear();

            context?.SaveChanges();
        }

        private Task PublishPendingEventsAsync(DbContext? context, CancellationToken cancellationToken)
        {
            domainEventService.EmitEvents(_pendingOutboxMessages);
            _pendingOutboxMessages.Clear();

            return context is not null ? context.SaveChangesAsync(cancellationToken) : Task.CompletedTask;
        }
    }
}
