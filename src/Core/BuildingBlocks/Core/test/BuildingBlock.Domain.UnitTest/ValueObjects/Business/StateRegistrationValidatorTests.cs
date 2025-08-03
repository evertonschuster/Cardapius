using BuildingBlock.Domain.ValueObjects.Business;

namespace BuildingBlock.Domain.UnitTest.ValueObjects.Business;

public class StateRegistrationValidatorTests
{
    [Fact]
    public void Validate_Should_Fail_When_Empty()
    {
        var result = StateRegistrationValidator.Validate("");
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_Should_Succeed_When_Valid()
    {
        var result = StateRegistrationValidator.Validate("123456");
        result.IsValid.Should().BeTrue();
    }
}
