using BuildingBlock.Domain.ValueObjects.Emails;

namespace BuildingBlock.Domain.UnitTest.ValueObjects.Contact
{
    public class EmailValidatorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Validate_DeveRetornarErro_QuandoEmailVazio(string? email)
        {
            var result = EmailValidator.Validate(email);

            result.IsValid.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
            result.FirstError.Should().Be("O e-mail não pode estar vazio.");
        }

        [Theory]
        [InlineData("a@b")]
        [InlineData("x@y")]
        public void Validate_DeveRetornarErro_QuandoEmailMuitoCurto(string email)
        {
            var result = EmailValidator.Validate(email);

            result.IsValid.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
            result.FirstError.Should().Be("O e-mail é muito curto.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoEmailMuitoLongo()
        {
            var email = new string('a', 250) + "@test.com"; // >254 caracteres
            var result = EmailValidator.Validate(email);

            result.IsValid.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
            result.FirstError.Should().Be("O e-mail é muito longo.");
        }

        [Theory]
        [InlineData("plainaddress")]
        [InlineData("missingatsign.com")]
        [InlineData("missingdomain@")]
        [InlineData("@missinguser.com")]
        [InlineData("user@.com")]
        [InlineData("user@com")]
        public void Validate_DeveRetornarErro_QuandoFormatoInvalido(string email)
        {
            var result = EmailValidator.Validate(email);

            result.IsValid.Should().BeFalse();
            result.FirstError.Should().Be("O e-mail informado é inválido.");
        }

        [Theory]
        [InlineData("user@example.com")]
        [InlineData("user.name+tag+sorting@example.com")]
        [InlineData("user_name@example.co.uk")]
        [InlineData("user-name@sub.domain.com")]
        public void Validate_DeveRetornarSucesso_QuandoEmailValido(string email)
        {
            var result = EmailValidator.Validate(email);

            result.IsValid.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            result.FirstError.Should().BeNull();
        }
    }
}
