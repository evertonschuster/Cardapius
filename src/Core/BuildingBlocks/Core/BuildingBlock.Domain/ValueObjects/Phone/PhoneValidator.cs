using BuildingBlock.Domain.ValueObjects.Phones.Exceptions;
using System.Text.RegularExpressions;

namespace BuildingBlock.Domain.ValueObjects.Phones
{
    internal static partial class PhoneValidator
    {
        private static readonly Regex _phoneRegex = PhoneValidatorRegex();

        public static ValidationResult<string> IsValid(string? phone)
        {
            string phoneNumber = _phoneRegex.Replace(phone ?? "", "");

            if (phoneNumber.Length < 10 || phoneNumber.Length > 11)
            {
                var error = new ValidationError(new InvalidPhoneException());
                return ValidationResult<string>.Failure(phone, error);
            }

            return ValidationResult<string>.Success(phoneNumber!);
        }

        [GeneratedRegex("[^0-9]")]
        private static partial Regex PhoneValidatorRegex();
    }
}
