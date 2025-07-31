using BuildingBlock.Domain.ValueObjects.Emails;

namespace BuildingBlock.Domain.ValueObjects.Contact
{
    public readonly struct Email : IValueObject<string, Email>
    {
        public static string Empty { get => "meunome@email.com"; }

        private Email(string email)
        {
            Value = email;
        }

        public string Value { get; init; }


        /// <summary>
        /// Attempts to create an <see cref="Email"/> value object from the provided string, validating its format.
        /// </summary>
        /// <param name="email">The email address to parse and validate.</param>
        /// <returns>A <see cref="Result{Email}"/> containing the valid <see cref="Email"/> if successful, or a failure result if validation fails.</returns>
        public static Result<Email> Parse(string? email)
        {
            var result = EmailValidator.Validate(email);
            return Result<Email>.FromValidation(result, () => new Email(email!));
        }

        /// <summary>
        /// Returns the string representation of the email address.
        /// </summary>
        /// <returns>The email address as a string.</returns>
        public override string ToString()
        {
            return Value;
        }
    }
}
