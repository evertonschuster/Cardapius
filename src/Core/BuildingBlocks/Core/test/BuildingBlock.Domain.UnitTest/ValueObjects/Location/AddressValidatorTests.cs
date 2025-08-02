using BuildingBlock.Domain.ValueObjects.Location;

namespace BuildingBlock.Domain.UnitTest.ValueObjects.Location
{
    public class AddressValidatorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Validate_DeveRetornarErro_QuandoLogradouroVazio(string? street)
        {
            var result = AddressValidator.Validate(street, "10", null, "Cidade", "SP", "12345-678");
            result.IsValid.Should().BeFalse();
            result.FirstError.Should().Be("O endereço (logradouro) não pode estar vazio.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoLogradouroMuitoLongo()
        {
            var street = new string('a', 201);
            var result = AddressValidator.Validate(street, "10", null, "Cidade", "SP", "12345-678");
            result.IsValid.Should().BeFalse();
            result.FirstError.Should().Be("O endereço deve ter no máximo 200 caracteres.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Validate_DeveRetornarErro_QuandoNumeroVazio(string? number)
        {
            var result = AddressValidator.Validate("Rua", number, null, "Cidade", "SP", "12345-678");
            result.IsValid.Should().BeFalse();
            result.FirstError.Should().Be("O número não pode estar vazio.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoNumeroMuitoLongo()
        {
            var number = new string('1', 21);
            var result = AddressValidator.Validate("Rua", number, null, "Cidade", "SP", "12345-678");
            result.IsValid.Should().BeFalse();
            result.FirstError.Should().Be("O número deve ter no máximo 20 caracteres.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoComplementoMuitoLongo()
        {
            var complement = new string('c', 101);
            var result = AddressValidator.Validate("Rua", "10", complement, "Cidade", "SP", "12345-678");
            result.IsValid.Should().BeFalse();
            result.FirstError.Should().Be("O complemento deve ter no máximo 100 caracteres.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Validate_DeveRetornarErro_QuandoCidadeVazia(string? city)
        {
            var result = AddressValidator.Validate("Rua", "10", null, city, "SP", "12345-678");
            result.IsValid.Should().BeFalse();
            result.FirstError.Should().Be("A cidade não pode estar vazia.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoCidadeMuitoLonga()
        {
            var city = new string('c', 101);
            var result = AddressValidator.Validate("Rua", "10", null, city, "SP", "12345-678");
            result.IsValid.Should().BeFalse();
            result.FirstError.Should().Be("A cidade deve ter no máximo 100 caracteres.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Validate_DeveRetornarErro_QuandoEstadoVazio(string? state)
        {
            var result = AddressValidator.Validate("Rua", "10", null, "Cidade", state, "12345-678");
            result.IsValid.Should().BeFalse();
            result.FirstError.Should().Be("O estado não pode estar vazio.");
        }

        [Theory]
        [InlineData("S")]
        [InlineData("SPO")]
        [InlineData("123")]
        public void Validate_DeveRetornarErro_QuandoEstadoComTamanhoInvalido(string state)
        {
            var result = AddressValidator.Validate("Rua", "10", null, "Cidade", state, "12345-678");
            result.IsValid.Should().BeFalse();
            result.FirstError.Should().Be("O estado deve ter exatamente 2 caracteres (sigla).");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Validate_DeveRetornarErro_QuandoCepVazio(string? zip)
        {
            var result = AddressValidator.Validate("Rua", "10", null, "Cidade", "SP", zip);
            result.IsValid.Should().BeFalse();
            result.FirstError.Should().Be("O CEP não pode estar vazio.");
        }

        [Theory]
        [InlineData("1234-567")]
        [InlineData("1234567")]
        [InlineData("abcde-fgh")]
        [InlineData("12345-6789")]
        public void Validate_DeveRetornarErro_QuandoCepInvalido(string zip)
        {
            var result = AddressValidator.Validate("Rua", "10", null, "Cidade", "SP", zip);
            result.IsValid.Should().BeFalse();
            result.FirstError.Should().Be("O CEP é inválido. Formato esperado: 00000-000 ou 00000000.");
        }

        [Theory]
        [InlineData("12345-678")]
        [InlineData("12345678")]
        public void Validate_DeveRetornarSucesso_QuandoTodosCamposValidos(string zip)
        {
            var result = AddressValidator.Validate("Rua", "10", "Apto 1", "Cidade", "SP", zip);
            result.IsValid.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            result.FirstError.Should().BeNull();
        }
    }
}
