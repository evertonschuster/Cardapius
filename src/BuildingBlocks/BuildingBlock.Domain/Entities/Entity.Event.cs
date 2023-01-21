using BuildingBlock.Domain.Events;

namespace BuildingBlock.Domain.Entities
{
    public abstract partial class Entity
    {
        private List<IDomainEvent>? _domainEvents;


        public void AddDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents = _domainEvents ?? new List<IDomainEvent>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public IReadOnlyCollection<IDomainEvent>? DomainEvents => _domainEvents?.AsReadOnly();
    }
}
