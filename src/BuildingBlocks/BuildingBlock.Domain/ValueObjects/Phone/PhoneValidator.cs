using System.Text.RegularExpressions;

namespace BuildingBlock.Domain.ValueObjects.Phones
{
    internal partial class PhoneValidator
    {
        protected PhoneValidator()
        {
        }

        private static readonly Regex _phoneRegex = PhoneValidatorRegex();

        public static bool IsValid(string? phone)
        {
            string phoneNumber = _phoneRegex.Replace(phone ?? "", "");

            if (phoneNumber.Length < 10 || phoneNumber.Length > 11)
            {
                return false;
            }

            return true;
        }

        [GeneratedRegex("[^0-9]")]
        private static partial Regex PhoneValidatorRegex();
    }
}
