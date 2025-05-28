using BuildingBlock.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BuildingBlock.Api.Domain.ValueObjects.Json.Validators
{
    internal class ValueObjectValidationModelValidator : IModelValidator
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            if (context.Model is not IValueObject model)
                yield break;

            var validationResult = model.Validate();
            if (validationResult.IsFailure)
            {
                yield return new ModelValidationResult(context.ModelMetadata.Name, validationResult.GetMessageErrors());
            }
        }
    }
}
