using BuildingBlock.Domain.ValueObjects;

namespace Store.Domain.Products.Entities
{
    public record ProductSubItem : ValueObject
    {
        public bool HasItem => Items?.Count > 0;

        public int? Min { get; init; }

        public int? Max { get; init; }

        public List<SubItem>? Items { get; init; }
    }
}
