namespace BuildingBlock.Domain.ValueObjects
{
    public interface IValidatable
    {
        /// <summary>
/// Validates the current object and returns the result of the validation.
/// </summary>
/// <returns>A <see cref="Result"/> indicating whether the object is valid.</returns>
Result Validate();
    }
}
