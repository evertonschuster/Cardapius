namespace Administration.Domain.Products.ValueObjects
{
    public record ProductSubItem : ValueObject
    {
        public bool HasItem => Items?.Count > 0;

        public int? Min { get; init; }

        public int? Max { get; init; }

        public List<SubItem>? Items { get; init; }
    }
}
