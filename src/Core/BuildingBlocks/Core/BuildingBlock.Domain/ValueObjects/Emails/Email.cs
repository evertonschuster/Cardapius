namespace BuildingBlock.Domain.ValueObjects.Emails
{
    public readonly struct Email : IValueObject<string, Email>
    {
        public static string Empty { get => "meunome@email.com"; }

        private Email(string email)
        {
            Value = email;
        }

        public string Value { get; init; }


        public static Email Parse(string? email)
        {
            var result = EmailValidator.IsValid(email);
            result.ThrowIfInvalid();

            return new Email(email!);
        }

        public override string ToString()
        {
            return this.Value;
        }

        public ValidationResult<string> IsValid()
        {
            return EmailValidator.IsValid(this.Value);
        }
    }
}
