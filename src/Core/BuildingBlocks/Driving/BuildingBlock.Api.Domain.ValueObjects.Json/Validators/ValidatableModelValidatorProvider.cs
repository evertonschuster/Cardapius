using BuildingBlock.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BuildingBlock.Api.Domain.ValueObjects.Json.Validators
{
    public class ValidatableModelValidatorProvider : IModelValidatorProvider
    {
        public void CreateValidators(ModelValidatorProviderContext context)
        {
            var modelType = context.ModelMetadata.ModelType;
            var implementIValidatable = modelType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidatable<>));

            if (implementIValidatable)
            {
                context.Results.Add(new ValidatorItem
                {
                    Validator = new ValidatableModelValidator(),
                    IsReusable = true,
                });
            }
        }
    }
}
