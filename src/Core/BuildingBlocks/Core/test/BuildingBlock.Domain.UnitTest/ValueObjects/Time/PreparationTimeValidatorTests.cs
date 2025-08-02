using BuildingBlock.Domain.ValueObjects.Time;

namespace BuildingBlock.Domain.UnitTest.ValueObjects.Time
{
    public class PreparationTimeValidatorTests
    {
        [Fact]
        public void Validate_DeveRetornarErro_QuandoTempoNaoInformado()
        {
            // Arrange
            TimeSpan? preparationTime = null;

            // Act
            var result = PreparationTimeValidator.Validate(preparationTime);

            // Assert
            result.IsValid.Should().BeFalse();
            result.FirstError.Should().Be("O tempo de preparação deve ser informado.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoTempoNegativo()
        {
            // Arrange
            TimeSpan? preparationTime = TimeSpan.FromMinutes(-5);

            // Act
            var result = PreparationTimeValidator.Validate(preparationTime);

            // Assert
            result.IsValid.Should().BeFalse();
            result.FirstError.Should().Be("O tempo de preparação não pode ser negativo.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(60)]
        public void Validate_DeveRetornarSucesso_QuandoTempoValido(int minutos)
        {
            // Arrange
            TimeSpan? preparationTime = TimeSpan.FromMinutes(minutos);

            // Act
            var result = PreparationTimeValidator.Validate(preparationTime);

            // Assert
            result.IsValid.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            result.FirstError.Should().BeNull();
        }
    }
}
