using BuildingBlock.Domain.Exceptions;
using BuildingBlock.Domain.Rules;

namespace BuildingBlock.Domain.ValueObjects
{
    public abstract record ValueObject : IEquatable<ValueObject>, IValueObject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Marcar membros como estáticos", Justification = "<Pendente>")]
        protected void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}