namespace BuildingBlock.Domain.ValueObjects
{
    public interface IValueObject
    {
        ValidationResult Validate();

        string? ToString();
    }

    public interface IValueObject<TValue> : IValueObject
    {
        new ValidationResult<TValue> Validate();

        ValidationResult IValueObject.Validate()
        {
            return Validate();
        }
    }

    public interface IValueObject<TValue, TType> : IValueObject<TValue>
    {
        static abstract TType Parse(TValue? s);
    }
}