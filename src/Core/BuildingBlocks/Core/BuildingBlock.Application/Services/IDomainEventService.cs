using BuildingBlock.Application.Entities;
using BuildingBlock.Domain.Entities;

namespace BuildingBlock.Application.Services
{
    public interface IDomainEventService
    {
        void EmitEvents(IEnumerable<OutboxMessageEntity> events);
        List<OutboxMessageEntity> GetDamainOutboxEvents(IEnumerable<IAggregateRoot> models);
    }
}
