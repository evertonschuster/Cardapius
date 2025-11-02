using BuildingBlock.Application.Entities;

namespace BuildingBlock.Application.Repositories
{
    public interface IOutboxMessageRepository
    {
        void Insert(List<OutboxMessageEntity> outboxMessages);
        void Update(List<OutboxMessageEntity> outboxMessages);
    }
}
