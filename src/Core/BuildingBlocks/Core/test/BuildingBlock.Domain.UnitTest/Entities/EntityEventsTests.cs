using BuildingBlock.Domain.Entities;
using BuildingBlock.Domain.Events;

namespace BuildingBlock.Domain.UnitTest.Entities
{
    public class EntityEventsTests
    {
        [Fact]
        public void AddNewEventSuccess()
        {
            //Arrange
            var entity = new EntityFake(Guid.NewGuid());


            //Act
            Action act = () => entity.Make();


            //Asserts
            act.Should()
                .NotThrow();

            entity.DomainEvents
                .Should()
                .HaveCount(1);
        }


        private class EntityFake : Entity
        {
            public EntityFake(Guid id) : base(id)
            {
            }

            internal void Make()
            {
                this.AddDomainEvent(new MakeDomainEvent());
            }
        }

        private class MakeDomainEvent : IDomainEvent
        {
            public MakeDomainEvent()
            {
                this.Id = Guid.NewGuid();
                this.OccurredOn = DateTime.Now;
            }

            public Guid Id { get; init; }

            public DateTimeOffset OccurredOn { get; init; }
        }
    }
}
