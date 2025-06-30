using BuildingBlock.Domain.ValueObjects.Products;

namespace Administration.Domain.Products.ValueObjects
{
    public record SubItem : ValueObject
    {
        public required ProductName Name { get; init; }

        public string? Description { get; init; }

        public required List<string> Images { get; init; }

        public SalePrice? Price { get; init; }
    }
}
