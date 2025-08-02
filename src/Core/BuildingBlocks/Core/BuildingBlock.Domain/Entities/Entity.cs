using BuildingBlock.Domain.Events;
using BuildingBlock.Domain.Exceptions;
using BuildingBlock.Domain.Rules;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace BuildingBlock.Domain.Entities
{
    public abstract class Entity : IEqualityComparer<Entity>, IAggregateRoot, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class with the specified unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier for the entity.</param>
        protected Entity(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class without setting the identifier.
        /// </summary>
        protected Entity()
        {
        }

        public Guid Id { get; init; }
        public DateTimeOffset? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }

        private List<IDomainEvent>? _domainEvents;

        /// <summary>
        /// Determines whether the specified object is an <see cref="Entity"/> and has the same identifier as the current instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current entity.</param>
        /// <returns><c>true</c> if the specified object is an <see cref="Entity"/> with the same <c>Id</c>; otherwise, <c>false</c>.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is not Entity)
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            Entity item = (Entity)obj;

            return item.Id == this.Id;
        }

        /// <summary>
        /// Determines whether the current entity is equal to another entity based on reference or identifier equality.
        /// </summary>
        /// <param name="other">The entity to compare with the current entity.</param>
        /// <returns><c>true</c> if the entities are the same instance or have the same identifier; otherwise, <c>false</c>.</returns>
        public bool Equals(Entity? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return this.Equals(other as object);
        }

        /// <summary>
        /// Determines whether two Entity instances are the same reference.
        /// </summary>
        /// <param name="x">The first Entity to compare.</param>
        /// <param name="y">The second Entity to compare.</param>
        /// <returns>True if both references point to the same object or are both null; otherwise, false.</returns>
        public bool Equals(Entity? x, Entity? y)
        {
            return x == y;
        }

        /// <summary>
        /// Enforces a business rule by throwing a <see cref="BusinessRuleValidationException"/> if the specified rule is broken.
        /// </summary>
        /// <param name="rule">The business rule to validate.</param>
        public void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }



        /// <summary>
        /// Adds a domain event to the entity's list of domain events.
        /// </summary>
        /// <param name="eventItem">The domain event to associate with the entity.</param>
        public void AddDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents ??= [];
            _domainEvents.Add(eventItem);
        }

        /// <summary>
        /// Removes the specified domain event from the entity's list of domain events, if present.
        /// </summary>
        /// <param name="eventItem">The domain event to remove.</param>
        public void RemoveDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        /// <summary>
        /// Removes all domain events associated with the entity.
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }


        /// <summary>
        /// Returns a read-only collection of domain events associated with the entity.
        /// </summary>
        /// <returns>A read-only collection of domain events, or an empty collection if none exist.</returns>
        public IReadOnlyCollection<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents ?? [];
        }

        /// <summary>
        /// Returns the runtime hash code for the specified entity instance.
        /// </summary>
        /// <param name="obj">The entity for which to compute the hash code. Must not be null.</param>
        /// <returns>The runtime hash code of the provided entity.</returns>
        public int GetHashCode([DisallowNull] Entity obj)
        {
            return RuntimeHelpers.GetHashCode(obj);
        }

        /// <summary>
/// Returns the runtime hash code for the current entity instance.
/// </summary>
/// <returns>The runtime hash code of this object.</returns>
public override int GetHashCode() => RuntimeHelpers.GetHashCode(this);
    }
}
