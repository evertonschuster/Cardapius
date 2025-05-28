using BuildingBlock.Domain.ValueObjects.ProductNames.Exceptions;

namespace BuildingBlock.Domain.ValueObjects.ProductNames
{
    internal static class ProductNameValidator
    {
        private const int MinLength = 2;
        private const int MaxLength = 100;

        internal static ValidationResult<string> Validate(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                var error = new ValidationError(new ProductNameEmptyException());
                return ValidationResult<string>.Failure(value, error);
            }

            var errors = new List<ValidationError>();
            var trimmed = value.Trim();
            if (trimmed.Length < MinLength)
            {
                errors.Add(new ValidationError(new ProductNameTooShortException(MinLength)));
            }
            if (trimmed.Length > MaxLength)
            {
                errors.Add(new ValidationError(new ProductNameTooLongException(MaxLength)));
            }

            if (errors.Count != 0)
            {
                return ValidationResult<string>.Failure(value, errors);
            }

            return ValidationResult<string>.Success(trimmed);
        }
    }
}
