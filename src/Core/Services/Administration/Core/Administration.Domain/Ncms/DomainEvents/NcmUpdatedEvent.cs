using BuildingBlock.Domain.Events;

namespace Administration.Domain.Ncms.DomainEvents;

internal class NcmUpdatedEvent<T> : IDomainEvent<T>
{
    public NcmUpdatedEvent(Guid id, T before, T after)
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
