using BuildingBlock.Domain.ValueObjects.Products;

namespace BuildingBlock.Domain.UnitTest.ValueObjects.Products
{
    public class ProductionPriceTests
    {
        [Theory]
        [InlineData(100, 10, 50)]
        [InlineData(0, 0, 0)]
        [InlineData(99999.99, 9999.99, 99999.99)]
        public void Validate_DeveRetornarSucesso_QuandoValoresValidos(decimal value, decimal maxDiscount, decimal productionCost)
        {
            var price = new ProductionPrice
            {
                Value = value,
                MaxDiscount = maxDiscount,
                ProductionCost = productionCost
            };

            var result = price.Validate();

            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Theory]
        [InlineData(-1, 10, 50, "Value")]
        [InlineData(100, -5, 50, "MaxDiscount")]
        [InlineData(100, 10, -20, "ProductionCost")]
        public void Validate_DeveRetornarErro_QuandoValoresInvalidos(decimal value, decimal maxDiscount, decimal productionCost, string propertyName)
        {
            var price = new ProductionPrice
            {
                Value = value,
                MaxDiscount = maxDiscount,
                ProductionCost = productionCost
            };

            var result = price.Validate();

            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
            result.Errors.Any(e => e.PropertyName == propertyName).Should().BeTrue();
        }
    }
}