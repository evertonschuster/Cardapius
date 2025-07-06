using BuildingBlock.Domain.ValueObjects;

namespace Administration.Domain.Products.ValueObjects
{
    public record ServesManyPeople : ValueObject
    {
        public int? Reference { get; init; }
        public int? Min { get; init; }
        public int? Max { get; init; }
    }
}
