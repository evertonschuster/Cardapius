using BuildingBlock.Domain.Exceptions;

namespace BuildingBlock.Domain.ValueObjects.ProductNames.Exceptions
{
    /// <summary>
    /// Thrown when the product or service name is shorter than the minimum length.
    /// </summary>
    public sealed class ProductNameTooLongException : BusinessException
    {
        public ProductNameTooLongException(int maxLength)
            : base($"Name cannot exceed {maxLength} characters.") { }
    }
}
