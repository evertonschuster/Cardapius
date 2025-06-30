namespace Administration.Domain.Products.ValueObjects
{
    public record ProductSubItem : ValueObject
    {
        public bool HasItem => Items?.Any() ?? false;

        public int? Min { get; init; }

        public int? Max { get; init; }

        public List<SubItem>? Items { get; init; }
    }
}
