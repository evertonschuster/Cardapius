using BuildingBlock.Domain.ValueObjects;

namespace Store.Domain.Products.Entities
{
    public record SubItem : ValueObject
    {
        public string Name { get; private set; }

        public string? Description { get; private set; }

        public List<string> Images { get; private set; }

        public double? Price { get; private set; }
    }
}
