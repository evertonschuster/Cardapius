namespace BuildingBlock.Domain.ValueObjects.Products
{
    internal static class SalePriceValidator
    {
        private const string EmptyPriceError = "O preço de venda deve ser informado.";
        private const string NegativePriceError = "O preço de venda não pode ser negativo.";
        private const string EmptyDiscountError = "O desconto máximo deve ser informado.";
        private const string NegativeDiscountError = "O desconto máximo não pode ser negativo.";
        private const string DiscountExceedsPriceError = "O desconto máximo não pode ser maior que o preço de venda.";

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