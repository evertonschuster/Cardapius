using BuildingBlock.Domain.ValueObjects.Emails.Exceptions;
using System.Text.RegularExpressions;

namespace BuildingBlock.Domain.ValueObjects.Emails
{
    internal static partial class EmailValidator
    {
        private static readonly Regex _emailRegex = EmailValidatorRegex();

        public static ValidationResult<string> IsValid(string? email)
        {
            var errors = new List<ValidationError>();
            if (string.IsNullOrWhiteSpace(email))
            {
                errors.Add(new ValidationError(new EmptyEmailException()));
            }

            if (email != null && !_emailRegex.IsMatch(email))
            {
                errors.Add(new ValidationError(new InvalidEmailException()));
            }

            if (errors.Count != 0)
            {
                return ValidationResult<string>.Failure(email, errors);
            }

            return ValidationResult<string>.Success(email!);
        }

        [GeneratedRegex("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$")]
        private static partial Regex EmailValidatorRegex();
    }
}
