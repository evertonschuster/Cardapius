
namespace BuildingBlock.Domain.ValueObjects
{
    public interface IResult<T>
    {
        IReadOnlyList<string> Errors { get; }
        bool IsSuccess { get; }
        T? Value { get; }
    }
}