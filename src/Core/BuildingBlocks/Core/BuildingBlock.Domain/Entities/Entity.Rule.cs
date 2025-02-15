using BuildingBlock.Domain.Exceptions;
using BuildingBlock.Domain.Rules;

namespace BuildingBlock.Domain.Entities
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S4035:Classes implementing \"IEquatable<T>\" should be sealed", Justification = "<Pendente>")]
    public abstract partial class Entity
    {
        protected void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
    }
}
