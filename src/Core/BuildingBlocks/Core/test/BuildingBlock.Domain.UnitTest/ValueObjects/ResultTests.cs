using BuildingBlock.Domain.ValueObjects;

namespace BuildingBlock.Domain.UnitTest.ValueObjects
{
    public class ResultTests
    {
        [Fact]
        public void Success_DeveRetornarResultComSucesso()
        {
            var result = Result.Success();

            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void Fail_DeveRetornarResultComErro()
        {
            var error = new ResultError("Campo", "Mensagem de erro");
            var result = Result.Fail(error);

            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().ContainSingle()
                .Which.Should().Be(error);
        }

        [Fact]
        public void Fail_DeveRetornarResultComVariosErros()
        {
            var error1 = new ResultError("Campo1", "Erro 1");
            var error2 = new ResultError("Campo2", "Erro 2");
            var result = Result.Fail(error1, error2);

            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().HaveCount(2);
            result.Errors.Should().Contain(error1);
            result.Errors.Should().Contain(error2);
        }

        [Fact]
        public void Fail_DeveRetornarResultComErroUsandoStrings()
        {
            var result = Result.Fail("Campo", "Erro 1", "Erro 2");

            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().HaveCount(2);
            result.Errors[0].PropertyName.Should().Be("Campo");
            result.Errors[0].Message.Should().Be("Erro 1");
            result.Errors[1].PropertyName.Should().Be("Campo");
            result.Errors[1].Message.Should().Be("Erro 2");
        }

        [Fact]
        public void Fail_DeveRetornarResultSemErrosQuandoNenhumErroInformado()
        {
            var result = Result.Fail();

            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void Fail_DeveRetornarResultSemErrosQuandoArrayDeStringsVazio()
        {
            var result = Result.Fail("Campo");

            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }
    }
}
