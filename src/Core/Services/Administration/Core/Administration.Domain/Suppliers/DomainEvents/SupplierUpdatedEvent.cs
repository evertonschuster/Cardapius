using BuildingBlock.Domain.Events;
using System;

namespace Administration.Domain.Suppliers.DomainEvents;

internal class SupplierUpdatedEvent<T> : IDomainEvent<T>
{
    public SupplierUpdatedEvent(Guid id, T before, T after)
    {
        Id = id;
        OccurredOn = DateTimeOffset.UtcNow;
        Before = before;
        After = after;
    }

    public Guid Id { get; }
    public DateTimeOffset OccurredOn { get; }
    public T Before { get; }
    public T After { get; }
}

