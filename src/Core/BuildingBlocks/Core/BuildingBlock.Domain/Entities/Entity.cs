
using BuildingBlock.Domain.Events;
using BuildingBlock.Domain.Exceptions;
using BuildingBlock.Domain.Rules;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace BuildingBlock.Domain.Entities
{
    public abstract class Entity : IEqualityComparer<Entity>, IAggregateRoot, ISoftDelete
    {
        protected Entity(Guid id)
        {
            Id = id;
        }

        protected Entity()
        {
        }

        public Guid Id { get; init; }
        public DateTimeOffset? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }

        private List<IDomainEvent>? _domainEvents;

        public override bool Equals(object? obj)
        {
            if (obj is not Entity)
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            Entity item = (Entity)obj;

            return item.Id == this.Id;
        }

        public bool Equals(Entity? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return this.Equals(other as object);
        }

        public bool Equals(Entity? x, Entity? y)
        {
            return x == y;
        }

        public void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }



        public void AddDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents ??= [];
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


        public IReadOnlyCollection<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents ?? [];
        }

        public int GetHashCode([DisallowNull] Entity obj)
        {
            return RuntimeHelpers.GetHashCode(obj);
        }

        public override int GetHashCode() => RuntimeHelpers.GetHashCode(this);
    }
}
