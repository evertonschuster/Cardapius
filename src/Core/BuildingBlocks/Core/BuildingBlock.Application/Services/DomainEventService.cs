using BuildingBlock.Application.Entities;
using BuildingBlock.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BuildingBlock.Application.Services
{
    internal class DomainEventService(IOptions<MvcNewtonsoftJsonOptions> jsonOptions) : IDomainEventService
    {
        /// <summary>
        /// Marks each provided outbox message entity as processed.
        /// </summary>
        /// <param name="events">A collection of outbox message entities to be marked as processed.</param>
        public void EmitEvents(IEnumerable<OutboxMessageEntity> events)
        {
            //Emitir eventos rabbit local
            foreach (var entity in events)
            {
                entity.Processed();
            }
        }

        /// <summary>
        /// Converts domain events from the provided aggregate root entities into a list of outbox message entities, serializing each event to JSON.
        /// </summary>
        /// <param name="models">A collection of aggregate root entities containing domain events.</param>
        /// <returns>A list of <see cref="OutboxMessageEntity"/> instances representing the serialized domain events.</returns>
        public List<OutboxMessageEntity> GetDomainOutboxEvents(IEnumerable<IAggregateRoot> models)
        {
            return models
            .SelectMany(entity =>
            {
                var events = entity.GetDomainEvents() ?? [];
                return events.Select(@event =>
                {
                    var json = JsonConvert.SerializeObject(@event, jsonOptions.Value.SerializerSettings);
                    return new OutboxMessageEntity
                    {
                        Id = Guid.CreateVersion7(),
                        EntityId = entity.Id,
                        EventId = @event.Id,
                        OccurredOn = @event.OccurredOn,
                        EntityType = entity.GetType().FullName!,
                        EventType = @event.GetType().FullName!,
                        Payload = json,
                    };
                });
            })
            .ToList();
        }
    }
}
