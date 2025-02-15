using BuildingBlock.Domain.ValueObjects;

namespace Store.Domain.Products.Entities
{
    public record ServesManyPeople : ValueObject
    {
        public int? Reference { get; private set; }
        public int? Min { get; private set; }
        public int? Max { get; private set; }
    }
}
