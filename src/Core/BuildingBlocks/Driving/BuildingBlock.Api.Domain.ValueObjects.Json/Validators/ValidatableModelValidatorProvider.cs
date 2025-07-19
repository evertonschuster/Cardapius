using BuildingBlock.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Concurrent;

namespace BuildingBlock.Api.Domain.ValueObjects.Json.Validators
{
    public class ValidatableModelValidatorProvider : IModelValidatorProvider
    {
        private static readonly Type ValidatableType = typeof(IValidatable);
        private static readonly ConcurrentDictionary<Type, bool> IsValidatableCache = new();

        private static readonly ValidatableModelValidator ValidatorInstance = new();

        public void CreateValidators(ModelValidatorProviderContext context)
        {
            var modelType = context.ModelMetadata.ModelType;
            if (modelType == null)
                return;

            if (!IsValidatableCache.GetOrAdd(modelType, t => ValidatableType.IsAssignableFrom(t)))
                return;

            context.Results.Add(new ValidatorItem
            {
                Validator = ValidatorInstance,
                IsReusable = true
            });
        }
    }
}
