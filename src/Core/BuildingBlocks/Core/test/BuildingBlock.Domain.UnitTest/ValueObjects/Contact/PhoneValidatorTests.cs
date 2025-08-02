using BuildingBlock.Domain.ValueObjects.Phones;

namespace BuildingBlock.Domain.UnitTest.ValueObjects.Contact
{
    public class PhoneValidatorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Validate_Should_Fail_When_Phone_Is_Null_Or_Empty(string? phone)
        {
            var result = PhoneValidator.Validate(phone);
            result.IsValid.Should().BeFalse();
            result.FirstError.Should().Be("O telefone não pode estar vazio.");
        }

        [Theory]
        [InlineData("1234567")]
        [InlineData("123")]
        public void Validate_Should_Fail_When_Phone_Is_Too_Short(string phone)
        {
            var result = PhoneValidator.Validate(phone);
            result.IsValid.Should().BeFalse();
            result.FirstError.Should().Be("O telefone deve ter ao menos 8 dígitos.");
        }

        [Theory]
        [InlineData("123456789012")]
        [InlineData("123456789012345")]
        public void Validate_Should_Fail_When_Phone_Is_Too_Long(string phone)
        {
            var result = PhoneValidator.Validate(phone);
            result.IsValid.Should().BeFalse();
            result.FirstError.Should().Be("O telefone deve ter no máximo 11 dígitos.");
        }

        [Theory]
        [InlineData("12345A78234")]
        [InlineData("99999-999X")]
        [InlineData("12#345678")]
        public void Validate_Should_Fail_When_Phone_Has_Invalid_Characters(string phone)
        {
            var result = PhoneValidator.Validate(phone);
            result.IsValid.Should().BeFalse();
            result.FirstError.Should().Be("O telefone contém caracteres inválidos.");
        }

        [Theory]
        [InlineData("0112345678")] // 10 dígitos, DDD começa com 0
        [InlineData("09123456789")] // 11 dígitos, DDD começa com 0
        public void Validate_Should_Fail_When_AreaCode_StartsWith_Zero(string phone)
        {
            var result = PhoneValidator.Validate(phone);
            result.IsValid.Should().BeFalse();
            result.FirstError.Should().Be("O código de área (DDD) é inválido.");
        }

        [Theory]
        [InlineData("91234567")] // 8 dígitos, fixo local começando com 9
        public void Validate_Should_Fail_When_Landline_StartsWith_9(string phone)
        {
            var result = PhoneValidator.Validate(phone);
            result.IsValid.Should().BeFalse();
            result.FirstError.Should().Be("Telefones fixos não devem começar com o dígito 9.");
        }

        [Theory]
        [InlineData("812345678")] // 9 dígitos, móvel local não começando com 9
        [InlineData("123456789")]
        public void Validate_Should_Fail_When_Mobile_Does_Not_StartWith_9(string phone)
        {
            var result = PhoneValidator.Validate(phone);
            result.IsValid.Should().BeFalse();
            result.FirstError.Should().Be("Telefones móveis devem começar com o dígito 9.");
        }

        [Theory]
        [InlineData("11912345678")] // 11 dígitos, terceiro dígito é 9 (móvel com DDD)
        [InlineData("21998765432")]
        [InlineData("31991234567")]
        public void Validate_Should_Succeed_For_Valid_Mobile_With_DDD(string phone)
        {
            var result = PhoneValidator.Validate(phone);
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("1134567890")] // 10 dígitos, terceiro dígito não é 9 (fixo com DDD)
        [InlineData("2132345678")]
        [InlineData("3131234567")]
        public void Validate_Should_Succeed_For_Valid_Landline_With_DDD(string phone)
        {
            var result = PhoneValidator.Validate(phone);
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("912345678")] // 9 dígitos, móvel local começando com 9
        public void Validate_Should_Succeed_For_Valid_Mobile_Local(string phone)
        {
            var result = PhoneValidator.Validate(phone);
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("12345678")] // 8 dígitos, fixo local não começando com 9
        [InlineData("23456789")]
        public void Validate_Should_Succeed_For_Valid_Landline_Local(string phone)
        {
            var result = PhoneValidator.Validate(phone);
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("1192345678")] // 10 dígitos, terceiro dígito é 9 (inválido para fixo)
        public void Validate_Should_Fail_When_Landline_With_DDD_Has_ThirdDigit9(string phone)
        {
            var result = PhoneValidator.Validate(phone);
            result.IsValid.Should().BeFalse();
            result.FirstError.Should().Be("Telefones fixos não devem começar com o dígito 9.");
        }

        [Theory]
        [InlineData("11823456789")] // 11 dígitos, terceiro dígito não é 9 (inválido para móvel)
        public void Validate_Should_Fail_When_Mobile_With_DDD_ThirdDigitNot9(string phone)
        {
            var result = PhoneValidator.Validate(phone);
            result.IsValid.Should().BeFalse();
            result.FirstError.Should().Be("Telefones móveis devem começar com o dígito 9.");
        }
    }
}
