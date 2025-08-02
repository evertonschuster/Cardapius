using BuildingBlock.Domain.Exceptions;
using BuildingBlock.Domain.ValueObjects;

namespace BuildingBlock.Domain.UnitTest.ValueObjects
{
    public class ValidationResultTests
    {
        [Fact]
        public void Success_DeveRetornarValidationResultValido()
        {
            var result = ValidationResult.Success();

            result.IsValid.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            result.Errors.Should().BeEmpty();
            result.FirstError.Should().BeNull();
            result.GetAllMessages().Should().BeNull();
        }

        [Fact]
        public void Failure_DeveRetornarValidationResultComErro()
        {
            var result = ValidationResult.Failure("campo", new[] { "mensagem de erro" });

            result.IsValid.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
            result.Errors.Should().NotBeNull();
            result.Errors.Should().ContainSingle();
            result.FirstError.Should().Be("mensagem de erro");
            result.GetAllMessages().Should().Be("mensagem de erro");
        }

        [Fact]
        public void Failure_DeveRetornarValidationResultComVariosErros()
        {
            var result = ValidationResult.Failure("campo", new[] { "erro 1", "erro 2" });

            result.IsValid.Should().BeFalse();
            result.Errors.Should().HaveCount(2);
            result.GetAllMessages().Should().Be("erro 1, erro 2");
        }

        [Fact]
        public void Failure_SemCampo_DeveRetornarValidationResultComErro()
        {
            var result = ValidationResult.Failure(new[] { "erro sem campo" });

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle();
            result.Errors![0].PropertyName.Should().BeNull();
            result.FirstError.Should().Be("erro sem campo");
        }

        [Fact]
        public void ThrowIfInvalid_DeveLancarExcecao_QuandoInvalido()
        {
            var result = ValidationResult.Failure("campo", new[] { "erro" });

            Action act = () => result.ThrowIfInvalid();

            act.Should().Throw<BusinessException>().WithMessage("erro");
        }

        [Fact]
        public void ThrowIfInvalid_NaoDeveLancarExcecao_QuandoValido()
        {
            var result = ValidationResult.Success();

            Action act = () => result.ThrowIfInvalid();

            act.Should().NotThrow();
        }


        [Fact]
        public void GetAllMessages_DeveRetornarNull_QuandoNaoHaErros()
        {
            var result = ValidationResult.Success();

            result.GetAllMessages().Should().BeNull();
        }
    }
}
