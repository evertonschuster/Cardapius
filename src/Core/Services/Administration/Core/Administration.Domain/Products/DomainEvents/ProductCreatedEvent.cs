using BuildingBlock.Domain.Events;
namespace Administration.Domain.Products.DomainEvents
{
    internal class ProductCreatedEvent<T> : IDomainEvent<T>
    {
        /// <summary>
        /// Initializes a new instance of the <c>ProductCreatedEvent&lt;T&gt;</c> class, capturing the entity's identifier, optional previous state, and new state at the time of creation.
        /// </summary>
        /// <param name="id">The unique identifier for the created entity.</param>
        /// <param name="before">The state of the entity before creation, or <c>null</c> if not applicable.</param>
        /// <param name="after">The state of the entity after creation.</param>
        public ProductCreatedEvent(Guid id, T? before, T after)
        {
            Id = id;
            OccurredOn = DateTimeOffset.UtcNow;
            Before = before;
            After = after;
        }

        public Guid Id { get; }

        public DateTimeOffset OccurredOn { get; }

        public T? Before { get; }

        public T After { get; }
    }
}
