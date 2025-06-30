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

        public static ValidationResult Validate(
            decimal? value,
            decimal? maxDiscount,
            decimal? productionCost)
        {
            if (!value.HasValue)
                return ValidationResult.Failure(EmptyPriceError);
            if (value.Value < 0)
                return ValidationResult.Failure(NegativePriceError);

            if (!maxDiscount.HasValue)
                return ValidationResult.Failure(EmptyDiscountError);
            if (maxDiscount.Value < 0)
                return ValidationResult.Failure(NegativeDiscountError);
            if (maxDiscount.Value > value.Value)
                return ValidationResult.Failure(DiscountExceedsPriceError);

            if (!productionCost.HasValue)
                return ValidationResult.Failure(EmptyCostError);
            if (productionCost.Value < 0)
                return ValidationResult.Failure(NegativeCostError);
            if (productionCost.Value > value.Value)
                return ValidationResult.Failure(CostExceedsPriceError);

            return ValidationResult.Success();
        }
    }
}
