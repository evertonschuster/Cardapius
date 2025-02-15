using BuildingBlock.Domain.ValueObjects;

namespace Store.Domain.Products.Entities
{
    public record ProductSubItem : ValueObject
    {
        public bool HasItem { get => Items?.Any() ?? false; }

        public int? Min { get; private set; }

        public int? Max { get; private set; }

        public List<SubItem>? Items { get; private set; }
    }
}
