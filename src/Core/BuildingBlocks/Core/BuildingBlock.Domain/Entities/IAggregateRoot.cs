using BuildingBlock.Domain.Events;
using BuildingBlock.Domain.Rules;

namespace BuildingBlock.Domain.Entities
{
    public interface IAggregateRoot
    {
        Guid Id { get; }

        void CheckRule(IBusinessRule rule);

        IReadOnlyCollection<IDomainEvent> GetDomainEvents();

        void AddDomainEvent(IDomainEvent eventItem);

        void RemoveDomainEvent(IDomainEvent eventItem);

        void ClearDomainEvents();
    }
}
