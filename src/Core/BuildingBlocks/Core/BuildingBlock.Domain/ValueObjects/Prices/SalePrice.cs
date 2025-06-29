namespace BuildingBlock.Domain.ValueObjects.Prices
{
    /// <summary>
    /// Representa o preço de venda de um produto ou serviço e seu desconto máximo.
    /// </summary>
    public readonly struct SalePrice : IValueObject, IValidatable<SalePrice>
    {
        public decimal Value { get; init; }
        public decimal MaxDiscount { get; init; }

        public SalePrice()
        {
            Value = 0;
            MaxDiscount = 0;
        }

        private SalePrice(decimal value, decimal maxDiscount)
        {
            Value = value;
            MaxDiscount = maxDiscount;
        }

        /// <summary>
        /// Cria uma instância validada de SalePrice.
        /// </summary>
        /// <param name="value">O valor bruto do preço de venda.</param>
        /// <param name="maxDiscount">O valor máximo de desconto permitido.</param>
        /// <returns>Um SalePrice imutável e validado.</returns>
        /// <exception cref="SalePriceEmptyException"></exception>
        /// <exception cref="SalePriceNegativeException"></exception>
        /// <exception cref="MaxDiscountEmptyException"></exception>
        /// <exception cref="MaxDiscountNegativeException"></exception>
        /// <exception cref="MaxDiscountExceedsPriceException"></exception>
        public static Result<SalePrice> Parse(decimal? value, decimal? maxDiscount)
        {
            var validation = SalePriceValidator.Validate(value, maxDiscount);
            return Result<SalePrice>.FromValidation(validation, () => new SalePrice(value!.Value, maxDiscount!.Value));
        }

        /// <summary>
        /// Validates the current <see cref="SalePrice"/> instance against the specified constraints.
        /// </summary>
        /// <remarks>This method uses the <see cref="SalePriceValidator"/> to validate the <see
        /// cref="Value"/> and  <see cref="MaxDiscount"/> properties of the current instance. The result indicates
        /// whether the  <see cref="SalePrice"/> is valid and provides details about any validation failures.</remarks>
        /// <returns>A <see cref="Result{T}"/> containing the validation outcome. If the validation succeeds, the result contains
        /// the current <see cref="SalePrice"/> instance; otherwise, it contains validation errors.</returns>
        public Result<SalePrice> Validate()
        {
            var validation = SalePriceValidator.Validate(Value, MaxDiscount);
            return Result<SalePrice>.FromValidation(validation, this);
        }
    }
}
