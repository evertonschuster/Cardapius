namespace BuildingBlock.Domain.ValueObjects.Prices
{
    /// <summary>
    /// Represents the production price of a product or service,
    /// including its maximum discount and production cost, as a Value Object.
    /// </summary>
    public readonly struct ProductionPrice : IValueObject, IValidatable<ProductionPrice>
    {
        public decimal Value { get; init; }
        public decimal MaxDiscount { get; init; }
        public decimal ProductionCost { get; init; }


        /// <summary>
        /// Validates this instance after deserialization.
        /// </summary>
        public Result<ProductionPrice> Validate()
        {
            var validation = ProductionPriceValidator
                .Validate(Value, MaxDiscount, ProductionCost);

            return Result<ProductionPrice>
                .FromValidation(validation, this);
        }
    }
}
