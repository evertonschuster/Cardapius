using BuildingBlock.Application.Entities;
using BuildingBlock.Domain.Entities;

namespace BuildingBlock.Application.Services
{
    public interface IDomainEventService
    {
        /// <summary>
        /// Retrieves domain outbox events from the provided aggregate root models.
        /// </summary>
        /// <param name="models">A collection of aggregate root instances to extract domain events from.</param>
        /// <returns>A list of outbox message entities representing the domain events.</returns>
        List<OutboxMessageEntity> GetDomainOutboxEvents(IEnumerable<IAggregateRoot> models);

        /// <summary>
        /// Stores domain events from outbox messages asynchronously.
        /// </summary>
        /// <param name="outboxMessages"></param>
        /// <returns></returns>
        Task StoreDomainEventsAsync(List<OutboxMessageEntity> outboxMessages);

        /// <summary>
        /// Processes and emits a collection of outbox message domain events.
        /// </summary>
        /// <param name="events">The outbox message events to be emitted.</param>
        Task EmitEventsAsync(List<OutboxMessageEntity> events);
    }
}