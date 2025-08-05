using BuildingBlock.Domain.ValueObjects.Business;

namespace BuildingBlock.Domain.UnitTest.ValueObjects.Business;

public class BankInformationValidatorTests
{
    [Fact]
    public void Validate_Should_Fail_When_Bank_Is_Empty()
    {
        var result = BankInformationValidator.Validate("", "1", "1", new[]{"pix"});
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public void Validate_Should_Succeed_When_Valid()
    {
        var result = BankInformationValidator.Validate("Banco", "0001", "123", new[]{"pix@teste.com"});
        result.IsSuccess.Should().BeTrue();
    }
}
