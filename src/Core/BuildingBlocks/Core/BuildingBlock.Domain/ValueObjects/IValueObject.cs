namespace BuildingBlock.Domain.ValueObjects
{
    [Obsolete("Use IValueObject<TValue, TType> instead. This interface will be removed in a future version.")]
    public interface IValueObject
    {
        bool IsValid();
    }

    public interface IValueObject<TValue>
    {
        ValidationResult<TValue> IsValid();

        string? ToString();
    }

    public interface IValueObject<TValue, TType> : IValueObject<TValue>
    {
        static abstract TType Parse(TValue? s);
    }
}