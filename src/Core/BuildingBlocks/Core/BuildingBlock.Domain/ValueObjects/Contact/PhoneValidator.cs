using System.Text.RegularExpressions;

namespace BuildingBlock.Domain.ValueObjects.Phones
{
    internal static partial class PhoneValidator
    {
        private const int MinLength = 8;
        private const int MaxLength = 11;

        private const string EmptyPhoneError = "O telefone não pode estar vazio.";
        private static readonly string TooShortError = $"O telefone deve ter ao menos {MinLength} dígitos.";
        private static readonly string TooLongError = $"O telefone deve ter no máximo {MaxLength} dígitos.";
        private const string InvalidCharacterError = "O telefone contém caracteres inválidos.";
        private const string InvalidAreaCodeError = "O código de área (DDD) é inválido.";
        private const string InvalidMobileError = "Telefones móveis devem começar com o dígito 9.";
        private const string InvalidLandlineError = "Telefones fixos não devem começar com o dígito 9.";

        [GeneratedRegex("[^0-9]")]
        private static partial Regex PhoneValidatorRegex();

        private static readonly Regex _phoneRegex = PhoneValidatorRegex();

        public static ValidationResult Validate(string? phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return ValidationResult.Failure(EmptyPhoneError);

            var digits = _phoneRegex.Replace(phone, "");

            if (digits.Length < MinLength)
                return ValidationResult.Failure(TooShortError);

            if (digits.Length > MaxLength)
                return ValidationResult.Failure(TooLongError);

            if (digits.Any(ch => !char.IsDigit(ch)))
                return ValidationResult.Failure(InvalidCharacterError);

            // Se houver DDD (10 ou 11 dígitos), valida o código de área
            if (digits.Length > 9 && digits[0] == '0')
            {
                return ValidationResult.Failure(InvalidAreaCodeError);
            }

            // Agora distinguimos móvel x fixo conforme a quantidade de dígitos
            switch (digits.Length)
            {
                // 8 dígitos: fixo local
                case 8:
                    if (digits[0] == '9')
                        return ValidationResult.Failure(InvalidLandlineError);
                    break;

                // 9 dígitos: móvel local
                case 9:
                    if (digits[0] != '9')
                        return ValidationResult.Failure(InvalidMobileError);
                    break;

                // 10 dígitos: fixo com DDD (terceiro dígito ≠ 9)
                case 10:
                    if (digits[2] == '9')
                        return ValidationResult.Failure(InvalidLandlineError);
                    break;

                // 11 dígitos: móvel com DDD (terceiro dígito = 9)
                case 11:
                    if (digits[2] != '9')
                        return ValidationResult.Failure(InvalidMobileError);
                    break;
            }

            return ValidationResult.Success();
        }
    }
}
