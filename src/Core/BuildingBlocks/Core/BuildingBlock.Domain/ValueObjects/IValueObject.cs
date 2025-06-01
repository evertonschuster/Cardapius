namespace BuildingBlock.Domain.ValueObjects
{
    public interface IValueObject
    {
        string? ToString();
    }

    public interface IValueObject<TValue, TType> : IValueObject
    {
        static abstract Result<TType> Parse(TValue? s);
    }
}