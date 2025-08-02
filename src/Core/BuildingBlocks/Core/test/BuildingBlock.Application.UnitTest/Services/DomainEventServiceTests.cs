using BuildingBlock.Application.Entities;
using BuildingBlock.Application.Services;
using BuildingBlock.Domain.Entities;
using BuildingBlock.Domain.Events;
using BuildingBlock.Domain.Rules;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace BuildingBlock.Application.UnitTest.Services
{
    public class DomainEventServiceTests
    {
        private class FakeDomainEvent : IDomainEvent
        {
            public Guid Id { get; set; } = Guid.NewGuid();
            public DateTimeOffset OccurredOn { get; set; } = DateTimeOffset.UtcNow;
        }

        private class FakeAggregateRoot : IAggregateRoot
        {
            public Guid Id { get; set; } = Guid.NewGuid();
            private readonly List<IDomainEvent> _events = new();

            public void CheckRule(IBusinessRule rule) { }
            public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _events;
            public void AddDomainEvent(IDomainEvent eventItem) => _events.Add(eventItem);
            public void RemoveDomainEvent(IDomainEvent eventItem) => _events.Remove(eventItem);
            public void ClearDomainEvents() => _events.Clear();
        }

        [Fact]
        public void GetDomainOutboxEvents_DeveRetornarOutboxMessages_ParaAggregateRoots()
        {
            // Arrange
            var fakeEvent = new FakeDomainEvent();
            var fakeAggregate = new FakeAggregateRoot();
            fakeAggregate.AddDomainEvent(fakeEvent);

            var jsonOptions = Substitute.For<IOptions<MvcNewtonsoftJsonOptions>>();
            jsonOptions.Value.Returns(new MvcNewtonsoftJsonOptions());

            var service = new DomainEventService(jsonOptions);

            // Act
            var result = service.GetDomainOutboxEvents(new[] { fakeAggregate });

            // Assert
            result.Should().HaveCount(1);
            var outbox = result.First();
            outbox.EntityId.Should().Be(fakeAggregate.Id);
            outbox.EventId.Should().Be(fakeEvent.Id);
            outbox.OccurredOn.Should().Be(fakeEvent.OccurredOn);
            outbox.EntityType.Should().Be(fakeAggregate.GetType().FullName);
            outbox.EventType.Should().Be(fakeEvent.GetType().FullName);
            outbox.Payload.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void EmitEvents_DeveChamarProcessedParaCadaEvento()
        {
            // Arrange
            var outbox = Substitute.For<OutboxMessageEntity>();
            var jsonOptions = Substitute.For<IOptions<MvcNewtonsoftJsonOptions>>();
            jsonOptions.Value.Returns(new MvcNewtonsoftJsonOptions());

            var service = new DomainEventService(jsonOptions);

            // Act
            service.EmitEvents(new[] { outbox });

            // Assert
            outbox.Received(1).Processed();
        }
    }
}
