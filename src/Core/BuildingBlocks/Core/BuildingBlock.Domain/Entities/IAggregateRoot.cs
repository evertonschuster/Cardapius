using BuildingBlock.Domain.Events;
using BuildingBlock.Domain.Rules;

namespace BuildingBlock.Domain.Entities
{
    public interface IAggregateRoot
    {
        Guid Id { get; }

        /// <summary>
/// Enforces a specified business rule on the aggregate root.
/// </summary>
/// <param name="rule">The business rule to be checked or enforced.</param>
void CheckRule(IBusinessRule rule);

        /// <summary>
/// Retrieves the collection of domain events associated with the aggregate root.
/// </summary>
/// <returns>A read-only collection of domain events.</returns>
IReadOnlyCollection<IDomainEvent> GetDomainEvents();

        /// <summary>
/// Adds a domain event to the aggregate root's collection of domain events.
/// </summary>
/// <param name="eventItem">The domain event to add.</param>
void AddDomainEvent(IDomainEvent eventItem);

        /// <summary>
/// Removes a specific domain event from the aggregate root's collection of domain events.
/// </summary>
/// <param name="eventItem">The domain event to remove.</param>
void RemoveDomainEvent(IDomainEvent eventItem);

        /// <summary>
/// Removes all domain events associated with the aggregate root.
/// </summary>
void ClearDomainEvents();
    }
}
