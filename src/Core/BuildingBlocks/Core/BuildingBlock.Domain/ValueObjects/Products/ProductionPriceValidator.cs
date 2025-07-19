namespace BuildingBlock.Domain.ValueObjects.Products
{
    internal static class ProductionPriceValidator
    {
        private const string EmptyPriceError = "Production price must be provided.";
        private const string NegativePriceError = "Production price cannot be negative.";
        private const string EmptyDiscountError = "Maximum discount must be provided.";
        private const string NegativeDiscountError = "Maximum discount cannot be negative.";
        private const string DiscountExceedsPriceError = "Maximum discount cannot exceed production price.";
        private const string EmptyCostError = "Production cost must be provided.";
        private const string NegativeCostError = "Production cost cannot be negative.";
        private const string CostExceedsPriceError = "Production cost cannot exceed production price.";

        public static Result<ProductionPrice> Validate(
            decimal? value,
            decimal? maxDiscount,
            decimal? productionCost)
        {
            if (!value.HasValue)
                return Result<ProductionPrice>.Fail(nameof(ProductionPrice.Value), EmptyPriceError);
            if (value.Value < 0)
                return Result<ProductionPrice>.Fail(nameof(ProductionPrice.Value), NegativePriceError);

            if (!maxDiscount.HasValue)
                return Result<ProductionPrice>.Fail(EmptyDiscountError);
            if (maxDiscount.Value < 0)
                return Result<ProductionPrice>.Fail(NegativeDiscountError);
            if (maxDiscount.Value > value.Value)
                return Result<ProductionPrice>.Fail(DiscountExceedsPriceError);

            if (!productionCost.HasValue)
                return Result<ProductionPrice>.Fail(EmptyCostError);
            if (productionCost.Value < 0)
                return Result<ProductionPrice>.Fail(NegativeCostError);
            if (productionCost.Value > value.Value)
                return Result<ProductionPrice>.Fail(CostExceedsPriceError);

            return Result<ProductionPrice>.Success();
        }
    }
}
