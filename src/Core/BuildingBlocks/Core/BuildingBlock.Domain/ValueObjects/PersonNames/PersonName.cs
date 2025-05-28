namespace BuildingBlock.Domain.ValueObjects.PersonNames
{
    public readonly struct PersonName : IValueObject<string, PersonName>
    {
        private PersonName(string name) : this()
        {
            Value = name;
        }

        public static string Empty { get => "Fulano de Tal"; }

        public string Value { get; init; }

        public static PersonName Parse(string? name)
        {
            var result = PersonNameValidator.IsValid(name);
            result.ThrowIfInvalid();

            return new PersonName(name);
        }

        public ValidationResult<string> Validate()
        {
            return PersonNameValidator.IsValid(this.Value);
        }

        public override string? ToString()
        {
            return Value;
        }
    }
}
