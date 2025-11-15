using BuildingBlock.Domain.ValueObjects.Business;

namespace BuildingBlock.Domain.UnitTest.ValueObjects.Business;

public class TradeNameValidatorTests
{
    [Fact]
    public void Validate_Should_Succeed_When_Valid()
    {
        var result = TradeNameValidator.Validate("Fantasia");
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Validate_Should_Fail_When_NullOrWhitespace(string? value)
    {
        var result = TradeNameValidator.Validate(value);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeNull();
        result.FirstError.Should().Be("O nome fantasia não pode estar vazio.");
    }

    [Theory]
    [InlineData("A")]
    [InlineData("1")]
    public void Validate_Should_Fail_When_TooShort(string value)
    {
        var result = TradeNameValidator.Validate(value);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeNull();
        result.FirstError.Should().Be("O nome fantasia deve ter ao menos 2 caracteres.");
    }

    [Fact]
    public void Validate_Should_Fail_When_TooLong()
    {
        var longName = new string('A', 151);
        var result = TradeNameValidator.Validate(longName);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeNull();
        result.FirstError.Should().Be("O nome fantasia deve ter no máximo 150 caracteres.");
    }

    [Theory]
    [InlineData("AB")]
    [InlineData("Nome Fantasia")]
    public void Validate_Should_Succeed_When_ValidLength(string value)
    {
        var result = TradeNameValidator.Validate(value);
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }
}