﻿using BuildingBlock.Domain.Entities;

namespace BuildingBlock.Domain.UnitTest.Entities
{
    public class EntityTests
    {

        [Fact]
        public void CompareEqualsEntityWithEqualsIdSuccess()
        {
            //Arrange
            var id = Guid.NewGuid();
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
            var entity1 = new EntityFake(Guid.NewGuid());


            //Act
            var isEquals = entity1.Equals(null);


            //Asserts
            isEquals.Should().BeFalse();
        }

        [Fact]
        public void CompareEqualsOperatorWithEqualsIdSuccess()
        {
            //Arrange
            var id = Guid.NewGuid();
            var entity1 = new EntityFake(id);
            var entity2 = new EntityFake(id);


            //Act
            var isEquals = entity1 == entity2;


            //Asserts
            isEquals.Should().BeTrue();
        }

        [Fact]
        public void CompareEqualsOperatorWithNullSuccess()
        {
            //Arrange
            EntityFake? entity1 = new(Guid.NewGuid());
            EntityFake? entity2 = null;


            //Act
            var isEquals1 = entity1 == entity2;
            var isEquals2 = entity2 == entity1;


            //Asserts
            isEquals1.Should().BeFalse();
            isEquals2.Should().BeFalse();
        }

        [Fact]
        public void CompareNotEqualsOperatorWithEqualsIdSuccess()
        {
            //Arrange
            var id = Guid.NewGuid();
            var entity1 = new EntityFake(id);
            var entity2 = new EntityFake(id);


            //Act
            var isEquals = entity1 != entity2;


            //Asserts
            isEquals.Should().BeFalse();
        }

        [Fact]
        public void CompareNotEqualsOperatorWithNullSuccess()
        {
            //Arrange
            EntityFake? entity1 = new(Guid.NewGuid());
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
            var entity1 = new EntityFake(Guid.NewGuid());
            var entity2 = new EntityFake(Guid.NewGuid());


            //Act
            var isEquals = entity1.Equals(entity2);


            //Asserts
            isEquals.Should().BeFalse();
        }

        [Fact]
        public void CompareEqualsOperatorWithDifferentIdSuccess()
        {
            //Arrange
            var entity1 = new EntityFake(Guid.NewGuid());
            var entity2 = new EntityFake(Guid.NewGuid());


            //Act
            var isEquals = entity1 == entity2;


            //Asserts
            isEquals.Should().BeFalse();
        }

        [Fact]
        public void CompareNotEqualsOperatorWithDifferentIdSuccess()
        {
            //Arrange
            var entity1 = new EntityFake(Guid.NewGuid());
            var entity2 = new EntityFake(Guid.NewGuid());


            //Act
            var isEquals = entity1 != entity2;


            //Asserts
            isEquals.Should().BeTrue();
        }


        [Fact]
        public void CompareEqualsReferenceEntityWithEqualsSuccess()
        {
            //Arrange
            var id = Guid.NewGuid();
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
            var id = Guid.NewGuid();
            var entity1 = new EntityFake(id);
            var entity2 = new EntityFake2(id);


            //Act
            var isEquals = entity1.Equals(entity2);


            //Asserts
            isEquals.Should().BeFalse();
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
    }
}
