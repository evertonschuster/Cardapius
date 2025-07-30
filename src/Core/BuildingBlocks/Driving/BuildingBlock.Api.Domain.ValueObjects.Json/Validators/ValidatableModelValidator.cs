using BuildingBlock.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BuildingBlock.Api.Domain.ValueObjects.Json.Validators
{
    public class ValidatableModelValidator : IModelValidator
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            if (context.Model is not IValidatable validatable)
                yield break;

            var result = validatable.Validate();
            if (result.IsSuccess)
                yield break;

            var memberName = context.ModelMetadata.Name ?? string.Empty;
            foreach (var err in result.Errors)
            {
                var name = string.IsNullOrEmpty(err.PropertyName) ? memberName : err.PropertyName;

                string formattedName;
                if (!string.IsNullOrEmpty(name) && char.IsUpper(name[0]))
                {
                    formattedName = string.Create(name.Length, name, (span, state) =>
                    {
                        span[0] = char.ToLowerInvariant(state[0]);
                        if (state.Length > 1)
                            state.AsSpan(1).CopyTo(span.Slice(1));
                    });
                }
                else
                {
                    formattedName = name;
                }

                yield return new ModelValidationResult(formattedName, err.Message);
            }
        }
    }
}
