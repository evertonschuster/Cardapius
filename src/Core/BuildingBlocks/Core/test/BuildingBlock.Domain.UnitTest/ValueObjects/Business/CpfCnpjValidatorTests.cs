using BuildingBlock.Domain.ValueObjects.Business;

namespace BuildingBlock.Domain.UnitTest.ValueObjects.Business;

public class CpfCnpjValidatorTests
{
    [Fact]
    public void Validate_Should_Fail_When_Empty()
    {
        var result = CpfCnpjValidator.Validate("");
        result.IsValid.Should().BeFalse();
    }

    [Theory]
    [InlineData("12345678901")] // CPF
    [InlineData("12345678000199")] // CNPJ
    public void Validate_Should_Succeed_When_Length_Is_Valid(string doc)
    {
        var result = CpfCnpjValidator.Validate(doc);
        result.IsValid.Should().BeTrue();
    }
}
