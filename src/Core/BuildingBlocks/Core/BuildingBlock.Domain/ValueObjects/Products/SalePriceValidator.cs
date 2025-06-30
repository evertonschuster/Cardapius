namespace BuildingBlock.Domain.ValueObjects.Products
{
    internal static class SalePriceValidator
    {
        private const string EmptyPriceError = "O preço de venda deve ser informado.";
        private const string NegativePriceError = "O preço de venda não pode ser negativo.";
        private const string EmptyDiscountError = "O desconto máximo deve ser informado.";
        private const string NegativeDiscountError = "O desconto máximo não pode ser negativo.";
        private const string DiscountExceedsPriceError = "O desconto máximo não pode ser maior que o preço de venda.";

        public static ValidationResult Validate(decimal? value, decimal? maxDiscount)
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

            return ValidationResult.Success();
        }
    }
}