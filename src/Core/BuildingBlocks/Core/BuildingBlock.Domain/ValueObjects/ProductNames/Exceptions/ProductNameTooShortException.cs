using BuildingBlock.Domain.Exceptions;

namespace BuildingBlock.Domain.ValueObjects.ProductNames.Exceptions
{
    /// <summary>
    /// Thrown when the product or service name exceeds the maximum length.
    /// </summary>
    public sealed class ProductNameTooShortException : BusinessException
    {
        public ProductNameTooShortException(int minLength)
            : base($"Name must be at least {minLength} characters long.") { }
    }
}
