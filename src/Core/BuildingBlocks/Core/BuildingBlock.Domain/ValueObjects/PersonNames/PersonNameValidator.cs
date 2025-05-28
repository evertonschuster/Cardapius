using BuildingBlock.Domain.ValueObjects.PersonNames.Exceptions;

namespace BuildingBlock.Domain.ValueObjects.PersonNames
{
    internal static class PersonNameValidator
    {
        public static ValidationResult<string> IsValid(string? name)
        {
            var errors = new List<ValidationError>();
            if (string.IsNullOrEmpty(name) || name.Length <= 5)
            {
                errors.Add(new ValidationError(new InvalidPersonNameException()));
            }

            if (name?.Split(" ").Length == 1)
            {
                errors.Add(new ValidationError(new InvalidPersonNameException()));
            }

            if (errors.Count != 0)
            {
                return ValidationResult<string>.Failure(name, errors);
            }

            return ValidationResult<string>.Success(name!);
        }
    }
}
