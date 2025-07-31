using BuildingBlock.Domain.ValueObjects.Phones;

namespace BuildingBlock.Domain.ValueObjects.Contact
{
    public readonly struct Phone : IValueObject<string, Phone>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Phone"/> struct with the specified phone number.
        /// </summary>
        /// <param name="phone">The phone number to assign to the value object.</param>
        private Phone(string phone) : this()
        {
            Value = phone;
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
