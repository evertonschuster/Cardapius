namespace BuildingBlock.Domain.Events
{
    public interface IDomainEvent : INotification
    {
        Guid Id { get; }

        DateTimeOffset OccurredOn { get; }
    }

    public interface IDomainEvent<out T> : IDomainEvent
    {
        T? Before { get; }
        T After { get; }
    }
}
