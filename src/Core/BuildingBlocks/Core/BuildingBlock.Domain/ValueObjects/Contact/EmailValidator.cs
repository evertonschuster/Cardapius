using System.Text.RegularExpressions;

namespace BuildingBlock.Domain.ValueObjects.Emails
{
    internal static partial class EmailValidator
    {
        private static readonly Regex _emailRegex = EmailRegex();

        private const int MinLength = 5;
        private const int MaxLength = 254;

        private const string EmptyEmailError = "O e-mail não pode estar vazio.";
        private const string InvalidFormatError = "O e-mail informado é inválido.";
        private const string TooShortError = "O e-mail é muito curto.";
        private const string TooLongError = "O e-mail é muito longo.";

        public static ValidationResult Validate(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return ValidationResult.Failure(errors: EmptyEmailError);

            if (email.Length < MinLength)
                return ValidationResult.Failure(errors: TooShortError);

            if (email.Length > MaxLength)
                return ValidationResult.Failure(errors: TooLongError);

            if (!_emailRegex.IsMatch(email))
                return ValidationResult.Failure(errors: InvalidFormatError);

            return ValidationResult.Success();
        }

        [GeneratedRegex("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$", RegexOptions.Compiled)]
        private static partial Regex EmailRegex();
    }
}