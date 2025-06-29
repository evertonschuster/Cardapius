namespace BuildingBlock.Domain.ValueObjects
{
    public interface IValidatable<TType>
    {
        Result<TType> Validate();
    }
}
