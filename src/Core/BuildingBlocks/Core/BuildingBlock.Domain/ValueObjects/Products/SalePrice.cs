namespace BuildingBlock.Domain.ValueObjects.Products
{
    /// <summary>
    /// Representa o preço de venda de um produto ou serviço e seu desconto máximo.
    /// </summary>
    public class SalePrice : IValueObject, IValidatable
    {
        public decimal Value { get; init; }
        public decimal MaxDiscount { get; init; }

        public SalePrice()
        {
            Value = 0;
            MaxDiscount = 0;
        }

        /// <summary>
        /// Validates the current <see cref="SalePrice"/> instance against the specified constraints.
        /// </summary>
        /// <remarks>This method uses the <see cref="SalePriceValidator"/> to validate the <see
        /// cref="Value"/> and  <see cref="MaxDiscount"/> properties of the current instance. The result indicates
        /// whether the  <see cref="SalePrice"/> is valid and provides details about any validation failures.</remarks>
        /// <returns>A <see cref="Result{T}"/> containing the validation outcome. If the validation succeeds, the result contains
        /// the current <see cref="SalePrice"/> instance; otherwise, it contains validation errors.</returns>
        public Result Validate()
        {
            return SalePriceValidator.Validate(Value, MaxDiscount);
        }
    }
}
