using BuildingBlock.Domain.ValueObjects.Business;

namespace BuildingBlock.Domain.UnitTest.ValueObjects.Business;

public class MunicipalRegistrationValidatorTests
{
    [Fact]
    public void Validate_Should_Fail_When_Empty()
    {
        var result = MunicipalRegistrationValidator.Validate("");
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_Should_Succeed_When_Valid()
    {
        var result = MunicipalRegistrationValidator.Validate("654321");
        result.IsValid.Should().BeTrue();
    }
}
