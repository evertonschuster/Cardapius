using System.Text.RegularExpressions;

namespace BuildingBlock.Domain.ValueObjects.Address
{
    internal static partial class AddressValidator
    {
        private const int MaxStreetLength = 200;
        private const int MaxNumberLength = 20;
        private const int MaxComplementLength = 100;
        private const int MaxCityLength = 100;
        private const int StateCodeLength = 2;

        private const string EmptyStreetError = "O endereço (logradouro) não pode estar vazio.";
        private static readonly string StreetTooLongError = $"O endereço deve ter no máximo {MaxStreetLength} caracteres.";

        private const string EmptyNumberError = "O número não pode estar vazio.";
        private static readonly string NumberTooLongError = $"O número deve ter no máximo {MaxNumberLength} caracteres.";

        private static readonly string ComplementTooLongError = $"O complemento deve ter no máximo {MaxComplementLength} caracteres.";

        private const string EmptyCityError = "A cidade não pode estar vazia.";
        private static readonly string CityTooLongError = $"A cidade deve ter no máximo {MaxCityLength} caracteres.";

        private const string EmptyStateError = "O estado não pode estar vazio.";
        private static readonly string StateInvalidError = $"O estado deve ter exatamente {StateCodeLength} caracteres (sigla).";

        private const string EmptyZipError = "O CEP não pode estar vazio.";
        private const string ZipInvalidError = "O CEP é inválido. Formato esperado: 00000-000 ou 00000000.";

        private static readonly Regex _zipRegex = ZipRegex();

        public static ValidationResult Validate(string? street, string? number, string? complement, string? city, string? state, string? zipCode)
        {
            if (string.IsNullOrWhiteSpace(street))
                return ValidationResult.Failure(EmptyStreetError);

            if (street.Length > MaxStreetLength)
                return ValidationResult.Failure(StreetTooLongError);

            if (string.IsNullOrWhiteSpace(number))
                return ValidationResult.Failure(EmptyNumberError);

            if (number.Length > MaxNumberLength)
                return ValidationResult.Failure(NumberTooLongError);

            if (!string.IsNullOrWhiteSpace(complement) && complement.Length > MaxComplementLength)
                return ValidationResult.Failure(ComplementTooLongError);

            if (string.IsNullOrWhiteSpace(city))
                return ValidationResult.Failure(EmptyCityError);

            if (city.Length > MaxCityLength)
                return ValidationResult.Failure(CityTooLongError);

            if (string.IsNullOrWhiteSpace(state))
                return ValidationResult.Failure(EmptyStateError);

            if (state.Length != StateCodeLength)
                return ValidationResult.Failure(StateInvalidError);

            if (string.IsNullOrWhiteSpace(zipCode))
                return ValidationResult.Failure(EmptyZipError);

            if (!_zipRegex.IsMatch(zipCode))
                return ValidationResult.Failure(ZipInvalidError);

            return ValidationResult.Success();
        }

        [GeneratedRegex(@"^\d{5}-?\d{3}$", RegexOptions.Compiled)]
        private static partial Regex ZipRegex();
    }
}
