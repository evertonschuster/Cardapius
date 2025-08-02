using BuildingBlock.Domain.ValueObjects.Products;

namespace BuildingBlock.Domain.UnitTest.ValueObjects.Products
{
    public class ProductionPriceValidatorTests
    {
        [Fact]
        public void Validate_DeveRetornarErro_QuandoPrecoVazio()
        {
            var result = ProductionPriceValidator.Validate(null, 10m, 5m);
            result.IsSuccess.Should().BeFalse();
            result.Errors[0].Message.Should().Be("O preço de produção deve ser informado.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoPrecoNegativo()
        {
            var result = ProductionPriceValidator.Validate(-1m, 10m, 5m);
            result.IsSuccess.Should().BeFalse();
            result.Errors[0].Message.Should().Be("O preço de produção não pode ser negativo.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoDescontoVazio()
        {
            var result = ProductionPriceValidator.Validate(10m, null, 5m);
            result.IsSuccess.Should().BeFalse();
            result.Errors[0].Message.Should().Be("O desconto máximo deve ser informado.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoDescontoNegativo()
        {
            var result = ProductionPriceValidator.Validate(10m, -1m, 5m);
            result.IsSuccess.Should().BeFalse();
            result.Errors[0].Message.Should().Be("O desconto máximo não pode ser negativo.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoDescontoMaiorQuePreco()
        {
            var result = ProductionPriceValidator.Validate(10m, 15m, 5m);
            result.IsSuccess.Should().BeFalse();
            result.Errors[0].Message.Should().Be("O desconto máximo não pode exceder o preço de produção.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoCustoVazio()
        {
            var result = ProductionPriceValidator.Validate(10m, 5m, null);
            result.IsSuccess.Should().BeFalse();
            result.Errors[0].Message.Should().Be("O custo de produção deve ser informado.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoCustoNegativo()
        {
            var result = ProductionPriceValidator.Validate(10m, 5m, -1m);
            result.IsSuccess.Should().BeFalse();
            result.Errors[0].Message.Should().Be("O custo de produção não pode ser negativo.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoCustoMaiorQuePreco()
        {
            var result = ProductionPriceValidator.Validate(10m, 5m, 15m);
            result.IsSuccess.Should().BeFalse();
            result.Errors[0].Message.Should().Be("O custo de produção não pode exceder o preço de produção.");
        }

        [Fact]
        public void Validate_DeveRetornarSucesso_QuandoTodosValoresValidos()
        {
            var result = ProductionPriceValidator.Validate(10m, 5m, 5m);
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }
    }
}
