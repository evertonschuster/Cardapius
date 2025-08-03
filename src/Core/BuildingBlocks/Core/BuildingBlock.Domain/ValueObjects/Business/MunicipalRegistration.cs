namespace BuildingBlock.Domain.ValueObjects.Business;

public readonly struct MunicipalRegistration : IValueObject<string, MunicipalRegistration>
{
    private MunicipalRegistration(string value) : this()
    {
        Value = value;
    }

    public string Value { get; init; }

    public static string Empty => "000000000";

    public static Result<MunicipalRegistration> Parse(string? value)
    {
        var result = MunicipalRegistrationValidator.Validate(value);
        return Result<MunicipalRegistration>.FromValidation(result, () => new MunicipalRegistration(value!));
    }

    public override string ToString() => Value;
}
