using BuildingBlock.Application.Entities;
using BuildingBlock.Domain.Entities;

namespace BuildingBlock.Application.Services
{
    public interface IDomainEventService
    {
        /// <summary>
        /// Processes and emits a collection of outbox message domain events.
        /// </summary>
        /// <param name="events">The outbox message events to be emitted.</param>
        void EmitEvents(IEnumerable<OutboxMessageEntity> events);
        /// <summary>
        /// Retrieves domain outbox events from the provided aggregate root models.
        /// </summary>
        /// <param name="models">A collection of aggregate root instances to extract domain events from.</param>
        /// <returns>A list of outbox message entities representing the domain events.</returns>
        List<OutboxMessageEntity> GetDomainOutboxEvents(IEnumerable<IAggregateRoot> models);

    }
}
