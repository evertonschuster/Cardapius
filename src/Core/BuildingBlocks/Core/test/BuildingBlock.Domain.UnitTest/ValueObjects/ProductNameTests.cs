using BuildingBlock.Domain.ValueObjects.ProductNames;
using BuildingBlock.Domain.ValueObjects.ProductNames.Exceptions;
using FluentAssertions;
using Xunit;

namespace BuildingBlock.Domain.UnitTest.ValueObjects
{
    public class ProductNameTests
    {
        [Fact]
        public void Create_ValidName_ReturnsProductName()
        {
            // Arrange
            var validName = "Produto Exemplo";

            // Act
            var productName = ProductName.Create(validName);

            // Assert
            productName.Value.Should().Be(validName);
            productName.ToString().Should().Be(validName);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_EmptyOrWhitespace_ThrowsProductNameEmptyException(string invalidName)
        {
            // Act
            Action act = () => ProductName.Create(invalidName);

            // Assert
            act.Should().Throw<ProductNameEmptyException>();
        }

        [Fact]
        public void Create_NameTooShort_ThrowsProductNameTooShortException()
        {
            // Arrange
            var shortName = "A"; // Supondo que o validador exija mais de 1 caractere

            // Act
            Action act = () => ProductName.Create(shortName);

            // Assert
            act.Should().Throw<ProductNameTooShortException>();
        }

        [Fact]
        public void Create_NameTooLong_ThrowsProductNameTooLongException()
        {
            // Arrange
            var longName = new string('A', 300); // Supondo que o validador tenha um limite inferior a 300

            // Act
            Action act = () => ProductName.Create(longName);

            // Assert
            act.Should().Throw<ProductNameTooLongException>();
        }

        [Fact]
        public void IsValid_ValidName_ReturnsValidResult()
        {
            // Arrange
            var validName = "Produto Válido";
            var productName = ProductName.Create(validName);

            // Act
            var result = productName.IsValid();

            // Assert
            result.IsValid.Should().BeTrue();
            result.Value.Should().Be(validName);
        }

    }
}
