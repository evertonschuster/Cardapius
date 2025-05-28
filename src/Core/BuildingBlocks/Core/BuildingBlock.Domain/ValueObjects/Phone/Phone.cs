namespace BuildingBlock.Domain.ValueObjects.Phones
{
    public readonly struct Phone : IValueObject<string, Phone>
    {
        private Phone(string phone) : this()
        {
            this.Value = phone;
        }

        public static string Empty { get => "45988293345"; }

        public string Value { get; init; }

        public static Phone Parse(string? phone)
        {
            var result = PhoneValidator.IsValid(phone);
            result.ThrowIfInvalid();

            return new Phone(phone!);
        }

        public ValidationResult<string> Validate()
        {
            return PhoneValidator.IsValid(Value);
        }

        public override string? ToString()
        {
            return Value;
        }
    }
}
