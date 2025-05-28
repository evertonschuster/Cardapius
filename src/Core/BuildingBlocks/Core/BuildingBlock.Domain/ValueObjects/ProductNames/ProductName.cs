namespace BuildingBlock.Domain.ValueObjects.ProductNames
{
    /// <summary>
    /// Represents the Name of a Product or Service as a Value Object.
    /// </summary>
    public readonly struct ProductName : IValueObject<string, ProductName>
    {
        public string Value { get; }
        private ProductName(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Creates a validated instance of ProductOrServiceName.
        /// </summary>
        /// <param name="value">The raw name text.</param>
        /// <returns>An immutable, validated ProductOrServiceName.</returns>
        /// <exception cref="ProductNameTooLongException"></exception>
        /// <exception cref="ProductNameTooShortException"></exception>
        /// <exception cref="ProductNameEmptyException"></exception>
        public static ProductName Parse(string? value)
        {
            var result = ProductNameValidator.Validate(value);
            result.ThrowIfInvalid();

            return new ProductName(value!.Trim());
        }

        public ValidationResult<string> IsValid()
        {
            return ProductNameValidator.Validate(Value);
        }

        public override string ToString() => Value;
    }
}
