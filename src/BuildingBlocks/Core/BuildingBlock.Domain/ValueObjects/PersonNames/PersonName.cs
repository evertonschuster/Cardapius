
using BuildingBlock.Domain.ValueObjects.PersonNames.Exceptions;

namespace BuildingBlock.Domain.ValueObjects.PersonNames
{
    public readonly struct PersonName : IValueObject
    {
        public PersonName(string? name) : this()
        {
            Value = name ?? throw new ArgumentNullException(nameof(name));
        }

        public static string Empty { get => "Fulano de Tal"; }

        public string Value { get; init; }

        public static PersonName Parse(string? name)
        {
            if (!PersonNameValidator.IsValid(name))
            {
                throw new InvalidPersonNameException();
            }

            return new PersonName(name);
        }

        public override string? ToString()
        {
            return Value;
        }

        public bool IsValid()
        {
            return PersonNameValidator.IsValid(this.Value);
        }
    }
}
