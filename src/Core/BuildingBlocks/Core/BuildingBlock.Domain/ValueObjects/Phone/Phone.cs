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

        public static Result<Phone> Parse(string? phone)
        {
            var result = PhoneValidator.Validate(phone);
            return Result<Phone>.FromValidation(result, () => new Phone(phone!));
        }

        public override string? ToString()
        {
            return Value;
        }
    }
}
