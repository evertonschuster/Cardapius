namespace BuildingBlock.Domain.ValueObjects.Products
{
    internal static class ProductNameValidator
    {
        private const int MinLength = 2;
        private const int MaxLength = 100;

        private const string EmptyNameError = "O nome do produto não pode estar vazio.";
        private static readonly string TooShortError = $"O nome do produto deve ter ao menos {MinLength} caracteres.";
        private static readonly string TooLongError = $"O nome do produto deve ter no máximo {MaxLength} caracteres.";
        private const string InvalidCharacterError = "O nome do produto contém caracteres inválidos.";

        public static ValidationResult Validate(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return ValidationResult.Failure(errors: EmptyNameError);

            var trimmed = value.Trim();

            if (trimmed.Length < MinLength)
                return ValidationResult.Failure(errors: TooShortError);

            if (trimmed.Length > MaxLength)
                return ValidationResult.Failure(errors: TooLongError);

            // Só permite letras, dígitos, espaço, hífen e sublinhado
            if (trimmed.Any(ch =>
                    !(char.IsLetterOrDigit(ch) || ch == ' ' || ch == '-' || ch == '_')))
            {
                return ValidationResult.Failure(errors: InvalidCharacterError);
            }

            return ValidationResult.Success();
        }
    }
}
