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
