namespace BuildingBlock.Domain.ValueObjects.Business;

public readonly struct StateRegistration : IValueObject<string, StateRegistration>
{
    private StateRegistration(string value) : this()
    {
        Value = value;
    }

    public string Value { get; init; }

    public static string Empty => "000000000";

    public static Result<StateRegistration> Parse(string? value)
    {
        var result = StateRegistrationValidator.Validate(value);
        return Result<StateRegistration>.FromValidation(result, () => new StateRegistration(value!));
    }

    public override string ToString() => Value;
}
