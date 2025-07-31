namespace BuildingBlock.Domain.ValueObjects.Products
{
    internal static class ProductionPriceValidator
    {
        private const string EmptyPriceError = "O preço de produção deve ser informado.";
        private const string NegativePriceError = "O preço de produção não pode ser negativo.";
        private const string EmptyDiscountError = "O desconto máximo deve ser informado.";
        private const string NegativeDiscountError = "O desconto máximo não pode ser negativo.";
        private const string DiscountExceedsPriceError = "O desconto máximo não pode exceder o preço de produção.";
        private const string EmptyCostError = "O custo de produção deve ser informado.";
        private const string NegativeCostError = "O custo de produção não pode ser negativo.";
        private const string CostExceedsPriceError = "O custo de produção não pode exceder o preço de produção.";

        /// <summary>
        /// Validates production price, maximum discount, and production cost values, ensuring they are provided, non-negative, and logically consistent.
        /// </summary>
        /// <param name="value">The production price to validate.</param>
        /// <param name="maxDiscount">The maximum allowable discount to validate.</param>
        /// <param name="productionCost">The production cost to validate.</param>
        /// <returns>
        /// A failure <c>Result</c> with the relevant property name and error message if any validation fails; otherwise, a success <c>Result</c>.
        /// </returns>
        public static Result Validate(
            decimal? value,
            decimal? maxDiscount,
            decimal? productionCost)
        {
            if (!value.HasValue)
                return Result.Fail(nameof(ProductionPrice.Value), EmptyPriceError);
            if (value.Value < 0)
                return Result.Fail(nameof(ProductionPrice.Value), NegativePriceError);

            if (!maxDiscount.HasValue)
                return Result.Fail(nameof(ProductionPrice.MaxDiscount), EmptyDiscountError);
            if (maxDiscount.Value < 0)
                return Result.Fail(nameof(ProductionPrice.MaxDiscount), NegativeDiscountError);
            if (maxDiscount.Value > value.Value)
                return Result.Fail(nameof(ProductionPrice.MaxDiscount), DiscountExceedsPriceError);

            if (!productionCost.HasValue)
                return Result.Fail(nameof(ProductionPrice.ProductionCost), EmptyCostError);
            if (productionCost.Value < 0)
                return Result.Fail(nameof(ProductionPrice.ProductionCost), NegativeCostError);
            if (productionCost.Value > value.Value)
                return Result.Fail(nameof(ProductionPrice.ProductionCost), CostExceedsPriceError);

            return Result.Success();
        }
    }
}
