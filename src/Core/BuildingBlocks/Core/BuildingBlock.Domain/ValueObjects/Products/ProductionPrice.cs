namespace BuildingBlock.Domain.ValueObjects.Products
{
    /// <summary>
    /// Represents the production price of a product or service,
    /// including its maximum discount and production cost, as a Value Object.
    /// </summary>
    public class ProductionPrice : IValueObject, IValidatable
    {
        public decimal Value { get; init; }
        public decimal MaxDiscount { get; init; }
        public decimal ProductionCost { get; init; }


        /// <summary>
        /// Validates this instance after deserialization.
        /// <summary>
        /// Validates the production price, maximum discount, and production cost values of this instance.
        /// </summary>
        /// <returns>A <see cref="Result"/> indicating whether the production price data is valid.</returns>
        public Result Validate()
        {
            return ProductionPriceValidator.Validate(Value, MaxDiscount, ProductionCost);
        }
    }
}
