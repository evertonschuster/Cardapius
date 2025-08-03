using BuildingBlock.Domain.ValueObjects.Business;

namespace BuildingBlock.Domain.UnitTest.ValueObjects.Business;

public class TradeNameValidatorTests
{
    [Fact]
    public void Validate_Should_Fail_When_Empty()
    {
        var result = TradeNameValidator.Validate("");
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_Should_Succeed_When_Valid()
    {
        var result = TradeNameValidator.Validate("Fantasia");
        result.IsValid.Should().BeTrue();
    }
}
