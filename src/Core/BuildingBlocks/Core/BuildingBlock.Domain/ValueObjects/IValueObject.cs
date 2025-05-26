namespace BuildingBlock.Domain.ValueObjects
{
    [Obsolete("Use IValueObject<T> instead. This interface will be removed in a future version.")]
    public interface IValueObject
    {
        bool IsValid();
    }

    public interface IValueObject<T>
    {
        ValidationResult<T> IsValid();

        string? ToString();
    }
}