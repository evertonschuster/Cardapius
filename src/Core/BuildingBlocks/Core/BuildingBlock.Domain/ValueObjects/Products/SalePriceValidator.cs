namespace BuildingBlock.Domain.ValueObjects.Products
{
    internal static class SalePriceValidator
    {
        private const string EmptyPriceError = "O preço de venda deve ser informado.";
        private const string NegativePriceError = "O preço de venda não pode ser negativo.";
        private const string EmptyDiscountError = "O desconto máximo deve ser informado.";
        private const string NegativeDiscountError = "O desconto máximo não pode ser negativo.";
        private const string DiscountExceedsPriceError = "O desconto máximo não pode ser maior que o preço de venda.";

        /// <summary>
        /// Validates the sale price and maximum discount values for correctness and logical consistency.
        /// </summary>
        /// <param name="value">The sale price to validate.</param>
        /// <param name="maxDiscount">The maximum discount to validate.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating success if both values are valid, or failure with an appropriate error message if validation fails.
        /// </returns>
        public static Result Validate(decimal? value, decimal? maxDiscount)
        {
            if (!value.HasValue)
                return Result.Fail(nameof(SalePrice.Value), EmptyPriceError);

            if (value.Value < 0)
                return Result.Fail(nameof(SalePrice.Value), NegativePriceError);

            if (!maxDiscount.HasValue)
                return Result.Fail(nameof(SalePrice.MaxDiscount), EmptyDiscountError);

            if (maxDiscount.Value < 0)
                return Result.Fail(nameof(SalePrice.MaxDiscount), NegativeDiscountError);

            if (maxDiscount.Value > value.Value)
                return Result.Fail(nameof(SalePrice.MaxDiscount), DiscountExceedsPriceError);

            return Result.Success();
        }
    }
}