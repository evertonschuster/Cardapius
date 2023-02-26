using BuildingBlock.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BuildingBlock.Domain.ValueObject.Json.Validators
{
    internal class ValueObjectValidationModelValidator : IModelValidator
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            if (context.Model is IValueObject model)
            {
                var isValid = model.IsValid();
                if (isValid is false)
                {
                    yield return new ModelValidationResult(null, "Deu ruim aqui na model");
                }
            }
        }
    }
}
