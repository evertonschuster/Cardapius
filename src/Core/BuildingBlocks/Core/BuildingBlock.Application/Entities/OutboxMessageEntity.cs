namespace BuildingBlock.Application.Entities
{
    public class OutboxMessageEntity
    {
        public required Guid Id { get; init; }
        public required Guid EventId { get; init; }
        public required Guid EntityId { get; init; }

        public required string EventType { get; init; }
        public required string EntityType { get; init; }
        public required string Payload { get; init; }

        public required DateTimeOffset OccurredOn { get; init; }

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
