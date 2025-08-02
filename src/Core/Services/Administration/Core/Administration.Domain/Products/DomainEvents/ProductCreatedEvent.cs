using BuildingBlock.Domain.Events;
namespace Administration.Domain.Products.DomainEvents
{
    internal class ProductCreatedEvent<T> : IDomainEvent<T>
    {
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
