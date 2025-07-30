using BuildingBlock.Domain.Entities;
using BuildingBlock.Domain.Exceptions;
using BuildingBlock.Domain.Rules;

namespace BuildingBlock.Domain.UnitTest.Entities
{
    public class EntityRulesTests
    {
        [Fact]
        public void CheckRuleWithValidRuleSuccess()
        {
            //Arrange
            var entity = new EntityFake(Guid.CreateVersion7());


            //Act
            Action act = () => entity.CheckValidRule();


            //Asserts
            act.Should()
                .NotThrow();
        }

        [Fact]
        public void CheckRuleWithInvalidRuleSuccess()
        {
            //Arrange
            var entity = new EntityFake(Guid.CreateVersion7());


            //Act
            Action act = () => entity.CheckInvalidRule();


            //Asserts
            act.Should()
                .Throw<BusinessRuleValidationException>();
        }

        private class EntityFake : Entity
        {
            public EntityFake(Guid id) : base(id)
            {
            }

            public void CheckValidRule()
            {
                var rule = new BusinessRuleFake(needBroken: false);
                this.CheckRule(rule);
            }

            public void CheckInvalidRule()
            {
                var rule = new BusinessRuleFake(needBroken: true);
                this.CheckRule(rule);
            }
        }

        private class BusinessRuleFake : IBusinessRule
        {
            public BusinessRuleFake(bool needBroken)
            {
                NeedBroken = needBroken;
            }

            public string Message => "A validação falhou!";

            public bool NeedBroken { get; }

            public bool IsBroken()
            {
                return NeedBroken;
            }
        }
    }
}
