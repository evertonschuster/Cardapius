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

        public static Result<PersonName> Parse(string? name)
        {
            var result = PersonNameValidator.Validate(name);
            return Result<PersonName>.FromValidation(result, () => new PersonName(name!));
        }

        public override string? ToString()
        {
            return Value;
        }
    }
}
