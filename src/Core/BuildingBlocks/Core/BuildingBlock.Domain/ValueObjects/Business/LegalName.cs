namespace BuildingBlock.Domain.ValueObjects.Business;

public readonly struct LegalName : IValueObject<string, LegalName>
{
    private LegalName(string value) : this()
    {
        Value = value;
    }

    public static string Empty => "Empresa Ltda";

    public string Value { get; init; }

    public static Result<LegalName> Parse(string? value)
    {
        var result = LegalNameValidator.Validate(value);
        return Result<LegalName>.FromValidation(result, () => new LegalName(value!));
    }

    public override string ToString() => Value;
}
