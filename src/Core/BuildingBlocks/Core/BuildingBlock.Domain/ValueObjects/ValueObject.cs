using BuildingBlock.Domain.Exceptions;
using BuildingBlock.Domain.Rules;

namespace BuildingBlock.Domain.ValueObjects
{
    public abstract record ValueObject : IEquatable<ValueObject>, IValueObject
    {
        protected static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }

        public ValidationResult Validate()
        {
            throw new NotImplementedException();
        }
    }
}