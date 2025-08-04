namespace BuildingBlock.Domain.ValueObjects.Business;

public readonly struct CpfCnpj : IValueObject<string, CpfCnpj>
{
    private CpfCnpj(string value) : this()
    {
        Value = value;
    }

    public string Value { get; init; }

    public static string Empty => "00000000000";

    public static Result<CpfCnpj> Parse(string? value)
    {
        var result = CpfCnpjValidator.Validate(value);
        return Result<CpfCnpj>.FromValidation(result, () => new CpfCnpj(value!));
    }

    public override string ToString() => Value;
}
