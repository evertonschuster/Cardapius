namespace BuildingBlock.Application.Entities
{
    public class OutboxMessageEntity
    {
        public Guid Id { get; init; }
        public Guid EventId { get; init; }
        public Guid EntityId { get; init; }

        public string EventType { get; init; }
        public string EntityType { get; init; }
        public string Payload { get; init; }

        public DateTimeOffset OccurredOn { get; init; }

        public DateTimeOffset? ProcessedAt { get; protected set; }
        public DateTimeOffset? SyncSendAt { get; protected set; }
        public DateTimeOffset? SynReceivedAt { get; init; }
        public string? SynReceivedFrom { get; init; }

        /// <summary>
        /// Marks the message as processed by setting the <c>ProcessedAt</c> timestamp to the current UTC time.
        /// </summary>
        internal void Processed()
        {
            ProcessedAt = DateTimeOffset.UtcNow;
        }
    }
}