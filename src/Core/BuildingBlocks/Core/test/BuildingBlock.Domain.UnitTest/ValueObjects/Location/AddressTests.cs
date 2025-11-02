using BuildingBlock.Domain.ValueObjects.Location;

namespace BuildingBlock.Domain.UnitTest.ValueObjects.Location
{
    public class AddressTests
    {
        [Fact]
        public void Empty_DeveRetornarEnderecoPadrao()
        {
            var address = Address.Empty;
            address.Street.Should().Be("Rua da silva");
            address.Number.Should().Be("01");
            address.Complement.Should().Be("Não tem");
            address.City.Should().Be("Cidade");
            address.State.Should().Be("Estado");
            address.ZIPCode.Should().Be("84589-000");
        }

        [Fact]
        public void Parse_DeveRetornarSucesso_QuandoDadosValidos()
        {
            var result = Address.Parse("Rua B", "10", "Casa", "CidadeY", "RJ", "12345-678");
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value!.Street.Should().Be("Rua B");
            result.Value.Number.Should().Be("10");
            result.Value.Complement.Should().Be("Casa");
            result.Value.City.Should().Be("CidadeY");
            result.Value.State.Should().Be("RJ");
            result.Value.ZIPCode.Should().Be("12345-678");
        }

        [Fact]
        public void Parse_DeveRetornarFalha_QuandoDadosInvalidos()
        {
            var result = Address.Parse(null, "10", null, "Cidade", "SP", "12345-678");
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public void ConstrutorPrivado_DeveInicializarComValoresPadrao()
        {
            // Usa reflexão para acessar o construtor privado
            var ctor = typeof(Address).GetConstructor(
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                null,
                Type.EmptyTypes,
                null);

            var address = (Address)ctor!.Invoke(null);

            address.Street.Should().BeEmpty();
            address.Number.Should().BeEmpty();
            address.Complement.Should().BeNull();
            address.City.Should().BeEmpty();
            address.State.Should().BeEmpty();
            address.ZIPCode.Should().BeEmpty();
        }
    }
}