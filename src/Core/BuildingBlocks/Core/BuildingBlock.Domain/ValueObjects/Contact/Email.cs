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


        public static Result<Email> Parse(string? email)
        {
            var result = EmailValidator.Validate(email);
            return Result<Email>.FromValidation(result, () => new Email(email));
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
