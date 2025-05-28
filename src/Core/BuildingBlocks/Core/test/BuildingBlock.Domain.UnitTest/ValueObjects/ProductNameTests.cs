using BuildingBlock.Domain.ValueObjects.ProductNames;
using BuildingBlock.Domain.ValueObjects.ProductNames.Exceptions;

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
            var productName = ProductName.Parse(validName);

            // Assert
            productName.Value.Should().Be(validName);
            productName.ToString().Should().Be(validName);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Create_EmptyOrWhitespace_ThrowsProductNameEmptyException(string? invalidName)
        {
            // Act
            Action act = () => ProductName.Parse(invalidName);

            // Assert
            act.Should().Throw<ProductNameEmptyException>();
        }

        [Fact]
        public void Create_NameTooShort_ThrowsProductNameTooShortException()
        {
            // Arrange
            var shortName = "A"; // Supondo que o validador exija mais de 1 caractere

            // Act
            Action act = () => ProductName.Parse(shortName);

            // Assert
            act.Should().Throw<ProductNameTooShortException>();
        }

        [Fact]
        public void Create_NameTooLong_ThrowsProductNameTooLongException()
        {
            // Arrange
            var longName = new string('A', 300); // Supondo que o validador tenha um limite inferior a 300

            // Act
            Action act = () => ProductName.Parse(longName);

            // Assert
            act.Should().Throw<ProductNameTooLongException>();
        }

        [Fact]
        public void IsValid_ValidName_ReturnsValidResult()
        {
            // Arrange
            var validName = "Produto Válido";
            var productName = ProductName.Parse(validName);

            // Act
            var result = productName.IsValid();

            // Assert
            result.IsValid.Should().BeTrue();
            result.Value.Should().Be(validName);
        }

    }
}
