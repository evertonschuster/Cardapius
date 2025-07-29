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

        /// <summary>
        /// Intercepts the save operation to collect domain events from aggregate roots before changes are persisted.
        /// </summary>
        /// <param name="eventData">The event data associated with the save operation.</param>
        /// <param name="result">The interception result from previous interceptors in the chain.</param>
        /// <returns>The interception result to continue or suppress the save operation.</returns>
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

        /// <summary>
        /// Intercepts the asynchronous save operation to collect domain events from aggregate roots before changes are persisted.
        /// </summary>
        /// <param name="eventData">The event data associated with the save operation.</param>
        /// <param name="result">The interception result for the save operation.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous interception operation, containing the interception result.</returns>
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

        /// <summary>
        /// Publishes any pending domain events after changes have been saved to the database.
        /// </summary>
        /// <param name="eventData">The event data associated with the completed save operation.</param>
        /// <param name="result">The result of the save operation.</param>
        /// <returns>The result of the save operation.</returns>
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

        /// <summary>
        /// Asynchronously publishes any collected domain events after changes have been saved to the database.
        /// </summary>
        /// <param name="eventData">The event data associated with the completed save operation.</param>
        /// <param name="result">The result of the save operation.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <returns>The result of the save operation.</returns>
        public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            if (_pendingOutboxMessages.Count == 0)
                return result;

            logger.LogDebug("Intercept SavedChangesAsync, publishing {Count} events", _pendingOutboxMessages.Count);
            await PublishPendingEventsAsync(eventData.Context, cancellationToken);

            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        /// <summary>
        /// Collects domain events from aggregate roots tracked by the DbContext, adds them to the context and the pending outbox list, and clears domain events from the aggregates.
        /// </summary>
        /// <param name="context">The DbContext from which to collect domain events.</param>
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

        /// <summary>
        /// Publishes all pending domain events and clears the pending event list, then saves changes to the provided DbContext if it is not null.
        /// </summary>
        private void PublishPendingEvents(DbContext? context)
        {
            domainEventService.EmitEvents(_pendingOutboxMessages);
            _pendingOutboxMessages.Clear();

            context?.SaveChanges();
        }

        /// <summary>
        /// Publishes all pending domain events asynchronously and persists any resulting changes to the database context.
        /// </summary>
        /// <param name="context">The DbContext to save changes to after publishing events, or null if no save is required.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the asynchronous operation to complete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private Task PublishPendingEventsAsync(DbContext? context, CancellationToken cancellationToken)
        {
            domainEventService.EmitEvents(_pendingOutboxMessages);
            _pendingOutboxMessages.Clear();

            return context is not null ? context.SaveChangesAsync(cancellationToken) : Task.CompletedTask;
        }
    }
}
