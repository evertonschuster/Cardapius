namespace BuildingBlock.Domain.ValueObjects.Products
{
    /// <summary>
    /// Represents the Name of a Product or Service as a Value Object.
    /// </summary>
    public readonly struct ProductName : IValueObject<string, ProductName>
    {
        public string Value { get; }
        public static string Empty { get => "Pizza de picanha"; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductName"/> struct with the specified product name value.
        /// </summary>
        /// <param name="value">The product name to assign to the value object.</param>
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
        public static Result<ProductName> Parse(string? value)
        {
            var result = ProductNameValidator.Validate(value);
            return Result<ProductName>.FromValidation(result, () => new ProductName(value!.Trim()));
        }


        public override string ToString() => Value;
    }
}
