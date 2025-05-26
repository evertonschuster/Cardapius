using BuildingBlock.Domain.Exceptions;

namespace BuildingBlock.Domain.ValueObjects.ProductNames.Exceptions
{

    /// <summary>
    /// Thrown when the product or service name is null, empty, or whitespace.
    /// </summary>
    public sealed class ProductNameEmptyException : BusinessException
    {
        public ProductNameEmptyException()
            : base("Name cannot be empty or whitespace.") { }
    }
}
