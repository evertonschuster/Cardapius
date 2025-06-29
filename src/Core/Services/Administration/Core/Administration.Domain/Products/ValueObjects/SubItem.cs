using BuildingBlock.Domain.ValueObjects;
using BuildingBlock.Domain.ValueObjects.Prices;
using BuildingBlock.Domain.ValueObjects.ProductNames;

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
