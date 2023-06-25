using BuildingBlock.Domain.ValueObjects.Phones.Exceptions;

namespace BuildingBlock.Domain.ValueObjects.Phones
{
    public readonly struct Phone : IValueObject
    {
        private Phone(string? phone) : this()
        {
            this.Value = phone ?? throw new ArgumentNullException(nameof(phone));
        }

        public static string Empty { get => "45988293345"; }

        public string Value { get; init; }

        public static Phone Parse(string? phone)
        {
            if (!PhoneValidator.IsValid(phone))
            {
                throw new InvalidPhoneException();
            }

            return new Phone(phone);
        }

        public bool IsValid()
        {
            return PhoneValidator.IsValid(Value);
        }
    }
}
