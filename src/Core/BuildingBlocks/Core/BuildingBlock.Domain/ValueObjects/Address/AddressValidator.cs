using BuildingBlock.Domain.ValueObjects.Address.Exceptions;
using System.Text.RegularExpressions;

namespace BuildingBlock.Domain.ValueObjects.Address
{
    internal static class AddressValidator
    {
        private const int MaxStreetLength = 200;
        private const int MaxNumberLength = 20;
        private const int MaxComplementLength = 100;
        private const int MaxCityLength = 100;
        private const int StateCodeLength = 2;
        private static readonly Regex ZipRegex = new(@"^\d{5}-?\d{3}$", RegexOptions.Compiled);

        internal static ValidationResult<string> IsValid(
            string? street,
            string? number,
            string? complement,
            string? city,
            string? state,
            string? zipCode)
        {
            var errors = new List<ValidationError>();

            var s = street?.Trim();
            if (string.IsNullOrEmpty(s))
                errors.Add(new ValidationError(new AddressStreetEmptyException()));
            else if (s.Length > MaxStreetLength)
                errors.Add(new ValidationError(new AddressStreetTooLongException(MaxStreetLength)));

            var n = number?.Trim();
            if (string.IsNullOrEmpty(n))
                errors.Add(new ValidationError(new AddressNumberEmptyException()));
            else if (n.Length > MaxNumberLength)
                errors.Add(new ValidationError(new AddressNumberTooLongException(MaxNumberLength)));

            var c = complement?.Trim();
            if (!string.IsNullOrEmpty(c) && c.Length > MaxComplementLength)
                errors.Add(new ValidationError(new AddressComplementTooLongException(MaxComplementLength)));

            var ci = city?.Trim();
            if (string.IsNullOrEmpty(ci))
                errors.Add(new ValidationError(new AddressCityEmptyException()));
            else if (ci.Length > MaxCityLength)
                errors.Add(new ValidationError(new AddressCityTooLongException(MaxCityLength)));

            var st = state?.Trim();
            if (string.IsNullOrEmpty(st))
                errors.Add(new ValidationError(new AddressStateEmptyException()));
            else if (st.Length != StateCodeLength)
                errors.Add(new ValidationError(new AddressStateInvalidException()));

            var z = zipCode?.Trim();
            if (string.IsNullOrEmpty(z))
                errors.Add(new ValidationError(new AddressZIPCodeEmptyException()));
            else if (!ZipRegex.IsMatch(z))
                errors.Add(new ValidationError(new AddressZIPCodeInvalidException()));

            var raw = $"{street};{number};{complement};{city};{state};{zipCode}";

            if (errors.Any())
                return ValidationResult<string>.Failure(raw, errors);

            return ValidationResult<string>.Success(raw);
        }
    }
}
