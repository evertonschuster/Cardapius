using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BuildingBlock.Domain.ValueObjects.Json.Validators
{
    internal sealed class ValueObjectModelValidatorProvider : IModelValidatorProvider
    {
        public void CreateValidators(ModelValidatorProviderContext context)
        {
            if (typeof(IValueObject).IsAssignableFrom(context.ModelMetadata.ModelType))
            {
                context.Results.Add(new ValidatorItem
                {
                    Validator = new ValueObjectValidationModelValidator(),
                });
            }
        }
    }
}
