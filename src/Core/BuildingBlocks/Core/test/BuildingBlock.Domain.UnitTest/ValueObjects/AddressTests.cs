using BuildingBlock.Domain.ValueObjects.Address;
using BuildingBlock.Domain.ValueObjects.Address.Exceptions;

namespace BuildingBlock.Domain.UnitTest.ValueObjects
{
    public class AddressTests
    {
        [Fact]
        public void Empty_ShouldReturnDefaultAddress()
        {
            var address = Address.Empty;

            Assert.Equal("Rua da silva", address.Street);
            Assert.Equal("01", address.Number);
            Assert.Equal("Não tem", address.Complement);
            Assert.Equal("Cidade", address.City);
            Assert.Equal("Estado", address.State);
            Assert.Equal("84589-000", address.ZIPCode);
        }

        [Fact]
        public void Parse_ValidParameters_ShouldReturnAddress()
        {
            var address = Address.Parse("Av. Brasil", "100", "Apto 10", "Curitiba", "PR", "80000-000");

            Assert.Equal("Av. Brasil", address.Street);
            Assert.Equal("100", address.Number);
            Assert.Equal("Apto 10", address.Complement);
            Assert.Equal("Curitiba", address.City);
            Assert.Equal("PR", address.State);
            Assert.Equal("80000-000", address.ZIPCode);
        }

        [Fact]
        public void ToString_ShouldReturnFormattedString()
        {
            var address = Address.Parse("Rua X", "123", null, "Cidade Y", "UF", "12345-678");
            var str = address.ToString();

            Assert.Contains("Street: Rua X", str);
            Assert.Contains("Number: 123", str);
            Assert.Contains("Complement: ", str);
            Assert.Contains("City: Cidade Y", str);
            Assert.Contains("State: UF", str);
            Assert.Contains("ZIPCode: 12345-678", str);
        }

        [Fact]
        public void IsValid_ShouldReturnSuccessForValidAddress()
        {
            var address = Address.Parse("Rua Z", "321", "Casa", "Cidade Z", "SP", "98765-432");
            var result = address.IsValid();

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Parse_InvalidParameters_ShouldThrowException()
        {
            Assert.ThrowsAny<Exception>(() =>
                Address.Parse(null, null, null, null, null, null)
            );
        }


        [Fact]
        public void Parse_StreetTooLong_DeveLancarAddressStreetTooLongException()
        {
            var longStreet = new string('A', 201);

            Assert.Throws<AddressStreetTooLongException>(() =>
                Address.Parse(longStreet, "10", "Comp", "Cidade", "SP", "12345-678")
            );
        }

        [Fact]
        public void Parse_NumberTooLong_DeveLancarAddressNumberTooLongException()
        {
            var longNumber = new string('1', 21);

            Assert.Throws<AddressNumberTooLongException>(() =>
                Address.Parse("Rua", longNumber, "Comp", "Cidade", "SP", "12345-678")
            );
        }

        [Fact]
        public void Parse_ComplementTooLong_DeveLancarAddressComplementTooLongException()
        {
            var longComplement = new string('C', 201);

            Assert.Throws<AddressComplementTooLongException>(() =>
                Address.Parse("Rua", "10", longComplement, "Cidade", "SP", "12345-678")
            );
        }

        [Fact]
        public void Parse_CityTooLong_DeveLancarAddressCityTooLongException()
        {
            var longCity = new string('X', 101);

            Assert.Throws<AddressCityTooLongException>(() =>
                Address.Parse("Rua", "10", "Comp", longCity, "SP", "12345-678")
            );
        }

        [Fact]
        public void Parse_StateInvalido_DeveLancarAddressStateInvalidException()
        {
            Assert.Throws<AddressStateInvalidException>(() =>
                Address.Parse("Rua", "10", "Comp", "Cidade", "ESTADOLONGO", "12345-678")
            );
        }

        [Fact]
        public void Parse_EnderecoValido_DeveRetornarAddress()
        {
            var address = Address.Parse("Rua Z", "321", "Casa", "Cidade Z", "SP", "98765-432");

            Assert.NotNull(address);
            Assert.Equal("Rua Z", address.Street);
            Assert.Equal("321", address.Number);
            Assert.Equal("Casa", address.Complement);
            Assert.Equal("Cidade Z", address.City);
            Assert.Equal("SP", address.State);
            Assert.Equal("98765-432", address.ZIPCode);
        }

        [Fact]
        public void IsValid_EnderecoValido_DeveRetornarValidationResultValido()
        {
            var address = Address.Parse("Rua Y", "123", "Apto", "Cidade Y", "RJ", "12345-000");
            var result = address.IsValid();

            Assert.True(result.IsValid);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public void Parse_ZipCodeInvalido_DeveLancarAddressZIPCodeInvalidException()
        {
            Assert.Throws<AddressZIPCodeInvalidException>(() =>
                Address.Parse("Rua", "10", "Comp", "Cidade", "SP", "123425678")
            );
        }
    }
}