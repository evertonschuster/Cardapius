using BuildingBlock.Domain.ValueObjects.Products;

namespace BuildingBlock.Domain.UnitTest.ValueObjects.Products
{
    public class SalePriceValidatorTests
    {
        [Fact]
        public void Validate_DeveRetornarErro_QuandoPrecoVazio()
        {
            var result = SalePriceValidator.Validate(null, 10m);
            result.IsSuccess.Should().BeFalse();
            result.Errors[0].Message.Should().Be("O preço de venda deve ser informado.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoPrecoNegativo()
        {
            var result = SalePriceValidator.Validate(-1m, 10m);
            result.IsSuccess.Should().BeFalse();
            result.Errors[0].Message.Should().Be("O preço de venda não pode ser negativo.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoDescontoVazio()
        {
            var result = SalePriceValidator.Validate(10m, null);
            result.IsSuccess.Should().BeFalse();
            result.Errors[0].Message.Should().Be("O desconto máximo deve ser informado.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoDescontoNegativo()
        {
            var result = SalePriceValidator.Validate(10m, -1m);
            result.IsSuccess.Should().BeFalse();
            result.Errors[0].Message.Should().Be("O desconto máximo não pode ser negativo.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoDescontoMaiorQuePreco()
        {
            var result = SalePriceValidator.Validate(10m, 20m);
            result.IsSuccess.Should().BeFalse();
            result.Errors[0].Message.Should().Be("O desconto máximo não pode ser maior que o preço de venda.");
        }

        [Fact]
        public void Validate_DeveRetornarSucesso_QuandoValoresValidos()
        {
            var result = SalePriceValidator.Validate(10m, 5m);
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void Validate_DeveRetornarSucesso_QuandoDescontoZero()
        {
            var result = SalePriceValidator.Validate(10m, 0m);
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void Validate_DeveRetornarSucesso_QuandoDescontoIgualAoPreco()
        {
            var result = SalePriceValidator.Validate(10m, 10m);
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }
    }
}
