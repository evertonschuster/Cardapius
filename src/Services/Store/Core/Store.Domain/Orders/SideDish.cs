using BuildingBlock.Domain.ValueObjects;

namespace Store.Domain.Orders
{
    public record SideDish : ValueObject
    {
        public int Quantity { get; private set; }
    }
}
