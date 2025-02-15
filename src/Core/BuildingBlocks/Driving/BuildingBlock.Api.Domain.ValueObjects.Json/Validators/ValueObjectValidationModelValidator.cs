using BuildingBlock.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BuildingBlock.Api.Domain.ValueObjects.Json.Validators
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
                    //TODO:
                    yield return new ModelValidationResult(context.ModelMetadata.Name, "Deu ruim aqui na model");
                }
            }
        }
    }
}
