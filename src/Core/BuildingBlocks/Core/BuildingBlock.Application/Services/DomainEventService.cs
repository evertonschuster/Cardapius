using BuildingBlock.Application.Entities;
using BuildingBlock.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BuildingBlock.Application.Services
{
    internal class DomainEventService(IOptions<MvcNewtonsoftJsonOptions> jsonOptions) : IDomainEventService
    {
        public void EmitEvents(IEnumerable<OutboxMessageEntity> events)
        {
            //Emitir eventos rabbit local
            foreach (var entity in events)
            {
                entity.Processed();
            }
        }

        public List<OutboxMessageEntity> GetDamainOutboxEvents(IEnumerable<IAggregateRoot> models)
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
