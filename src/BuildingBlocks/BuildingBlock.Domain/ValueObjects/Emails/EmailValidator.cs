using System.Text.RegularExpressions;

namespace BuildingBlock.Domain.ValueObjects.Emails
{
    internal partial class EmailValidator
    {
        protected EmailValidator()
        {

        }

        private static readonly Regex emailRegex = EmailValidatorRegex();

        public static bool IsValid(string? email)
        {
            if (string.IsNullOrEmpty(email)){
                return false;
            }

            return emailRegex.IsMatch(email);
        }

        [GeneratedRegex("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$")]
        private static partial Regex EmailValidatorRegex();
    }
}
