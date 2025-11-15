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

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Validate_Should_Fail_When_NullOrWhitespace(string? input)
    {
        var result = LegalNameValidator.Validate(input);
        result.IsValid.Should().BeFalse();
        result.FirstError.Should().Contain("não pode estar vazio");
    }

    [Theory]
    [InlineData("A")]
    public void Validate_Should_Fail_When_TooShort(string input)
    {
        var result = LegalNameValidator.Validate(input);
        result.IsValid.Should().BeFalse();
        result.FirstError.Should().Contain("ao menos");
    }

    [Fact]
    public void Validate_Should_Fail_When_TooLong()
    {
        var input = new string('A', 151);
        var result = LegalNameValidator.Validate(input);
        result.IsValid.Should().BeFalse();
        result.FirstError.Should().Contain("no máximo");
    }

    [Theory]
    [InlineData("AB")]
    [InlineData("Empresa LTDA")]
    [InlineData("  Empresa LTDA  ")]
    public void Validate_Should_Succeed_When_Valid(string input)
    {
        var result = LegalNameValidator.Validate(input);
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeNullOrEmpty();
    }
}