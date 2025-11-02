using BuildingBlock.Application.Entities;
using FluentAssertions;
using Xunit;

namespace BuildingBlock.Application.UnitTest.Entities
{
    public class OutboxMessageEntityTests
    {
        [Fact]
        public void Constructor_DeveInicializarPropriedadesObrigatorias()
        {
            var id = Guid.NewGuid();
            var eventId = Guid.NewGuid();
            var entityId = Guid.NewGuid();
            var occurredOn = DateTimeOffset.UtcNow;

            var entity = new OutboxMessageEntity
            {
                Id = id,
                EventId = eventId,
                EntityId = entityId,
                EventType = "TipoEvento",
                EntityType = "TipoEntidade",
                Payload = "{\"data\":123}",
                OccurredOn = occurredOn,
                SynReceivedAt = null,
                SynReceivedFrom = null
            };

            entity.Id.Should().Be(id);
            entity.EventId.Should().Be(eventId);
            entity.EntityId.Should().Be(entityId);
            entity.EventType.Should().Be("TipoEvento");
            entity.EntityType.Should().Be("TipoEntidade");
            entity.Payload.Should().Be("{\"data\":123}");
            entity.OccurredOn.Should().Be(occurredOn);
            entity.ProcessedAt.Should().BeNull();
            entity.SyncSendAt.Should().BeNull();
            entity.SynReceivedAt.Should().BeNull();
            entity.SynReceivedFrom.Should().BeNull();
        }

        [Fact]
        public void Processed_DeveDefinirProcessedAtComoUtcNow()
        {
            var entity = new OutboxMessageEntity
            {
                Id = Guid.NewGuid(),
                EventId = Guid.NewGuid(),
                EntityId = Guid.NewGuid(),
                EventType = "TipoEvento",
                EntityType = "TipoEntidade",
                Payload = "{}",
                OccurredOn = DateTimeOffset.UtcNow
            };

            entity.Processed();
            entity.ProcessedAt.Should().NotBeNull();
            entity.ProcessedAt.Value.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(2));
        }
    }
}