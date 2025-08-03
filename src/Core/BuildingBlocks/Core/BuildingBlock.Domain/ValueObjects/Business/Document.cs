namespace BuildingBlock.Domain.ValueObjects.Business;

public readonly struct Document : IValueObject<string, Document>
{
    private Document(string value) : this()
    {
        Value = value;
    }

    public string Value { get; init; }

    public static string Empty => "00000000000";

    public static Result<Document> Parse(string? value)
    {
        var result = DocumentValidator.Validate(value);
        return Result<Document>.FromValidation(result, () => new Document(value!));
    }

    public override string ToString() => Value;
}
