namespace BuildingBlock.Domain.ValueObjects.Business;

public readonly struct TradeName : IValueObject<string, TradeName>
{
    private TradeName(string value) : this()
    {
        Value = value;
    }

    public static string Empty => "Fantasia";

    public string Value { get; init; }

    public static Result<TradeName> Parse(string? value)
    {
        var result = TradeNameValidator.Validate(value);
        return Result<TradeName>.FromValidation(result, () => new TradeName(value!));
    }

    public override string ToString() => Value;
}
