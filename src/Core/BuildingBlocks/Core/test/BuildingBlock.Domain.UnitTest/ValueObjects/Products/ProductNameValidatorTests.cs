using BuildingBlock.Domain.ValueObjects.Products;

namespace BuildingBlock.Domain.UnitTest.ValueObjects.Products
{
    public class ProductNameValidatorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Validate_DeveRetornarErro_QuandoNomeVazio(string? name)
        {
            var result = ProductNameValidator.Validate(name);
            result.IsValid.Should().BeFalse();
            result.FirstError.Should().Be("O nome do produto não pode estar vazio.");
        }

        [Theory]
        [InlineData("A")]
        public void Validate_DeveRetornarErro_QuandoNomeMuitoCurto(string name)
        {
            var result = ProductNameValidator.Validate(name);
            result.IsValid.Should().BeFalse();
            result.FirstError.Should().Be("O nome do produto deve ter ao menos 2 caracteres.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoNomeMuitoLongo()
        {
            var name = new string('a', 101);
            var result = ProductNameValidator.Validate(name);
            result.IsValid.Should().BeFalse();
            result.FirstError.Should().Be("O nome do produto deve ter no máximo 100 caracteres.");
        }

        [Theory]
        [InlineData("Produto!")]
        [InlineData("Nome@Produto")]
        [InlineData("Produto#1")]
        [InlineData("Produto$")]
        [InlineData("Produto%")]
        [InlineData("Produto*")]
        [InlineData("Produto/")]
        [InlineData("Produto\\")]
        [InlineData("Produto,")]
        [InlineData("Produto.")]
        public void Validate_DeveRetornarErro_QuandoNomeContemCaracterInvalido(string name)
        {
            var result = ProductNameValidator.Validate(name);
            result.IsValid.Should().BeFalse();
            result.FirstError.Should().Be("O nome do produto contém caracteres inválidos.");
        }

        [Theory]
        [InlineData("Produto 1")]
        [InlineData("Produto-1")]
        [InlineData("Produto_1")]
        [InlineData("Produto Teste")]
        [InlineData("Produto123")]
        [InlineData("Produto_123-Teste")]
        [InlineData(" Produto ")]
        public void Validate_DeveRetornarSucesso_QuandoNomeValido(string name)
        {
            var result = ProductNameValidator.Validate(name);
            result.IsValid.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            result.FirstError.Should().BeNull();
        }
    }
}
