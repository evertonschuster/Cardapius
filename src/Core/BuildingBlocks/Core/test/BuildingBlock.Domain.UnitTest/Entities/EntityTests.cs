using BuildingBlock.Domain.Entities;
using BuildingBlock.Domain.Events;
using BuildingBlock.Domain.Exceptions;
using BuildingBlock.Domain.Rules;

namespace BuildingBlock.Domain.UnitTest.Entities
{
    public class EntityTests
    {

        [Fact]
        public void CompareEqualsEntityWithEqualsIdSuccess()
        {
            //Arrange
            var id = Guid.CreateVersion7();
            var entity1 = new EntityFake(id);
            var entity2 = new EntityFake(id);


            //Act
            var isEquals = entity1.Equals(entity2);


            //Asserts
            isEquals.Should().BeTrue();
        }

        [Fact]
        public void CompareEqualsEntityWithNullSuccess()
        {
            //Arrange
            var entity1 = new EntityFake(Guid.CreateVersion7());


            //Act
            var isEquals = entity1.Equals(null);


            //Asserts
            isEquals.Should().BeFalse();
        }


        [Fact]
        public void CompareEqualsOperatorWithNullSuccess()
        {
            //Arrange
            EntityFake? entity1 = new(Guid.CreateVersion7());
            EntityFake? entity2 = null;


            //Act
            var isEquals1 = entity1 == entity2;
            var isEquals2 = entity2 == entity1;


            //Asserts
            isEquals1.Should().BeFalse();
            isEquals2.Should().BeFalse();
        }

        [Fact]
        public void CompareNotEqualsOperatorWithNullSuccess()
        {
            //Arrange
            EntityFake? entity1 = new(Guid.CreateVersion7());
            EntityFake? entity2 = null;


            //Act
            var isEquals1 = entity1 != entity2;
            var isEquals2 = entity2 != entity1;


            //Asserts
            isEquals1.Should().BeTrue();
            isEquals2.Should().BeTrue();
        }

        [Fact]
        public void CompareEqualsEntityWithDifferentIdSuccess()
        {
            //Arrange
            var entity1 = new EntityFake(Guid.CreateVersion7());
            var entity2 = new EntityFake(Guid.CreateVersion7());


            //Act
            var isEquals = entity1.Equals(entity2);


            //Asserts
            isEquals.Should().BeFalse();
        }

        [Fact]
        public void CompareEqualsOperatorWithDifferentIdSuccess()
        {
            //Arrange
            var entity1 = new EntityFake(Guid.CreateVersion7());
            var entity2 = new EntityFake(Guid.CreateVersion7());


            //Act
            var isEquals = entity1 == entity2;


            //Asserts
            isEquals.Should().BeFalse();
        }

        [Fact]
        public void CompareNotEqualsOperatorWithDifferentIdSuccess()
        {
            //Arrange
            var entity1 = new EntityFake(Guid.CreateVersion7());
            var entity2 = new EntityFake(Guid.CreateVersion7());


            //Act
            var isEquals = entity1 != entity2;


            //Asserts
            isEquals.Should().BeTrue();
        }


        [Fact]
        public void CompareEqualsReferenceEntityWithEqualsSuccess()
        {
            //Arrange
            var id = Guid.CreateVersion7();
            var entity1 = new EntityFake(id);


            //Act
            var isEquals = entity1.Equals(entity1);


            //Asserts
            isEquals.Should().BeTrue();
        }

        [Fact]
        public void CompareWithAnotherEntityTypeWithEqualsIdSuccess()
        {
            //Arrange
            var id = Guid.CreateVersion7();
            var entity1 = new EntityFake(id);
            var entity2 = new EntityFake2(id);


            //Act
            var isEquals = entity1.Equals(entity2);


            //Asserts
            isEquals.Should().BeFalse();
        }
        [Fact]
        public void AddDomainEvent_ShouldAddEvent()
        {
            // Arrange
            var entity = new EntityFake(Guid.CreateVersion7());
            var domainEvent = new DomainEventFake();

            // Act
            entity.AddDomainEvent(domainEvent);

            // Assert
            entity.GetDomainEvents().Should().Contain(domainEvent);
        }

        [Fact]
        public void RemoveDomainEvent_ShouldRemoveEvent()
        {
            // Arrange
            var entity = new EntityFake(Guid.CreateVersion7());
            var domainEvent = new DomainEventFake();
            entity.AddDomainEvent(domainEvent);

            // Act
            entity.RemoveDomainEvent(domainEvent);

            // Assert
            entity.GetDomainEvents().Should().NotContain(domainEvent);
        }

        [Fact]
        public void ClearDomainEvents_ShouldRemoveAllEvents()
        {
            // Arrange
            var entity = new EntityFake(Guid.CreateVersion7());
            entity.AddDomainEvent(new DomainEventFake());
            entity.AddDomainEvent(new DomainEventFake());

            // Act
            entity.ClearDomainEvents();

            // Assert
            entity.GetDomainEvents().Should().BeEmpty();
        }

        [Fact]
        public void GetDomainEvents_ShouldReturnEmpty_WhenNoEventsAdded()
        {
            // Arrange
            var entity = new EntityFake(Guid.CreateVersion7());

            // Act
            var events = entity.GetDomainEvents();

            // Assert
            events.Should().BeEmpty();
        }

        [Fact]
        public void CheckRule_ShouldThrow_WhenRuleIsBroken()
        {
            // Arrange
            var entity = new EntityFake(Guid.CreateVersion7());
            var rule = new BrokenRuleFake();

            // Act
            Action act = () => entity.CheckRule(rule);

            // Assert
            act.Should().Throw<BusinessRuleValidationException>();
        }

        [Fact]
        public void CheckRule_ShouldNotThrow_WhenRuleIsNotBroken()
        {
            // Arrange
            var entity = new EntityFake(Guid.CreateVersion7());
            var rule = new NotBrokenRuleFake();

            // Act & Assert
            entity.Invoking(e => e.CheckRule(rule)).Should().NotThrow();
        }

        [Fact]
        public void GetHashCode_ShouldReturnSameValueForSameReference()
        {
            // Arrange
            var entity = new EntityFake(Guid.CreateVersion7());

            // Act
            var hash1 = entity.GetHashCode();
            var hash2 = entity.GetHashCode();

            // Assert
            hash1.Should().Be(hash2);
        }

        [Fact]
        public void GetHashCode_WithParameter_ShouldReturnSameValueForSameReference()
        {
            // Arrange
            var entity = new EntityFake(Guid.CreateVersion7());

            // Act
            var hash1 = entity.GetHashCode(entity);
            var hash2 = entity.GetHashCode(entity);

            // Assert
            hash1.Should().Be(hash2);
        }

        [Fact]
        public void Equals_WithDifferentTypes_ShouldReturnFalse()
        {
            // Arrange
            var entity = new EntityFake(Guid.CreateVersion7());
            var other = new object();

            // Act
            var result = entity.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_WithSameReference_ShouldReturnTrue()
        {
            // Arrange
            var entity = new EntityFake(Guid.CreateVersion7());

            // Act
            var result = entity.Equals((object)entity);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_WithNull_ShouldReturnFalse()
        {
            // Arrange
            var entity = new EntityFake(Guid.CreateVersion7());

            // Act
            var result = entity.Equals((Entity?)null);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_WithDifferentEntityInstancesWithSameId_ShouldReturnTrue()
        {
            // Arrange
            var id = Guid.CreateVersion7();
            var entity1 = new EntityFake(id);
            var entity2 = new EntityFake(id);

            // Act
            var result = entity1.Equals(entity2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_WithDifferentEntityInstancesWithDifferentId_ShouldReturnFalse()
        {
            // Arrange
            var entity1 = new EntityFake(Guid.CreateVersion7());
            var entity2 = new EntityFake(Guid.CreateVersion7());

            // Act
            var result = entity1.Equals(entity2);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_WithDifferentEntityTypesWithSameId_ShouldReturnFalse()
        {
            // Arrange
            var id = Guid.CreateVersion7();
            var entity1 = new EntityFake(id);
            var entity2 = new EntityFake2(id);

            // Act
            var result = entity1.Equals(entity2);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_WithSameReferenceEntity_ShouldReturnTrue()
        {
            // Arrange
            var entity = new EntityFake(Guid.CreateVersion7());

            // Act
            var result = entity.Equals(entity);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_WithNullEntitiesInComparer_ShouldReturnTrue()
        {
            // Arrange
            EntityFake? entity1 = null;
            EntityFake? entity2 = null;

            // Act
            var comparer = new EntityFake(Guid.CreateVersion7());
            var result = comparer.Equals(entity1, entity2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_WithOneNullEntityInComparer_ShouldReturnFalse()
        {
            // Arrange
            EntityFake? entity1 = new(Guid.CreateVersion7());
            EntityFake? entity2 = null;

            // Act
            var comparer = new EntityFake(Guid.CreateVersion7());
            var result1 = comparer.Equals(entity1, entity2);
            var result2 = comparer.Equals(entity2, entity1);

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
        }

        private class EntityFake : Entity
        {
            public EntityFake(Guid id) : base(id)
            {
            }
        }

        private class EntityFake2
        {
            public EntityFake2(Guid id)
            {
                this.Id = id;
            }

            public Guid Id { get; set; }
        }

        private class DomainEventFake : IDomainEvent
        {
            public Guid Id { get; } = Guid.NewGuid();
            public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
        }

        private class BrokenRuleFake : IBusinessRule
        {
            public bool IsBroken() => true;
            public string Message => "Broken";
        }

        private class NotBrokenRuleFake : IBusinessRule
        {
            public bool IsBroken() => false;
            public string Message => "Not broken";
        }
    }
}
