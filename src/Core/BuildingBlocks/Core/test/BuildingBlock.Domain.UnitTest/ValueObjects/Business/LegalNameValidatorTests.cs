using BuildingBlock.Domain.ValueObjects.Business;

namespace BuildingBlock.Domain.UnitTest.ValueObjects.Business;

public class LegalNameValidatorTests
{
    [Fact]
    public void Validate_Should_Fail_When_Empty()
    {
        var result = LegalNameValidator.Validate("");
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_Should_Succeed_When_Valid()
    {
        var result = LegalNameValidator.Validate("Empresa LTDA");
        result.IsValid.Should().BeTrue();
    }
}
