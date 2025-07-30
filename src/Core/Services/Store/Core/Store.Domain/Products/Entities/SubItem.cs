using BuildingBlock.Domain.ValueObjects;

namespace Store.Domain.Products.Entities
{
    public record SubItem : ValueObject
    {
        public string Name { get; init; }

        public string? Description { get; init; }

        public List<string> Images { get; init; }

        public double? Price { get; init; }
    }
}
