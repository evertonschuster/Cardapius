using BuildingBlock.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Concurrent;

namespace BuildingBlock.Api.Domain.ValueObjects.Json.Validators
{
    internal sealed class ValueObjectModelValidatorProvider : IModelValidatorProvider
    {
        private static readonly ConcurrentDictionary<Type, bool> _cache = new();

        public void CreateValidators(ModelValidatorProviderContext context)
        {
            var type = context.ModelMetadata.ModelType;

            if (_cache.GetOrAdd(type, t => typeof(IValueObject).IsAssignableFrom(t)))
            {
                context.Results.Add(new ValidatorItem
                {
                    Validator = new ValueObjectValidationModelValidator(),
                });
            }
        }
    }
}
