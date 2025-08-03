using BuildingBlock.Domain.Events;

namespace Administration.Domain.Suppliers.DomainEvents;

internal class SupplierCreatedEvent<T> : IDomainEvent<T>
{
    public SupplierCreatedEvent(Guid id, T? before, T after)
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
