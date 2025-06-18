namespace BuildingBlock.Domain.ValueObjects.PersonNames
{
    internal static class PersonNameValidator
    {
        private const int MinLength = 6;
        private const int MaxLength = 100;

        private const string EmptyNameError = "O nome não pode estar vazio.";
        private static readonly string TooShortError = $"O nome deve ter ao menos {MinLength} caracteres.";
        private static readonly string TooLongError = $"O nome deve ter no máximo {MaxLength} caracteres.";
        private const string MissingSurnameError = "O nome deve conter pelo menos nome e sobrenome.";
        private const string InvalidCharacterError = "O nome contém caracteres inválidos.";

        public static ValidationResult Validate(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return ValidationResult.Failure(EmptyNameError);

            var trimmed = name.Trim();
            if (trimmed.Length < MinLength)
                return ValidationResult.Failure(TooShortError);

            if (trimmed.Length > MaxLength)
                return ValidationResult.Failure(TooLongError);

            var parts = trimmed.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2)
                return ValidationResult.Failure(MissingSurnameError);

            if (parts.Any(part => part.Any(ch => !char.IsLetter(ch) && ch != '-' && ch != '\'')))
                return ValidationResult.Failure(InvalidCharacterError);

            return ValidationResult.Success();
        }
    }
}
