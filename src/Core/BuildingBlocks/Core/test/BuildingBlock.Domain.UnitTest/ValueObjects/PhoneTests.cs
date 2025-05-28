using BuildingBlock.Domain.Exceptions;
using BuildingBlock.Domain.ValueObjects.Phones;
using BuildingBlock.Domain.ValueObjects.Phones.Exceptions;

namespace BuildingBlock.Domain.UnitTest.ValueObjects
{
    public class PhoneTests
    {
        [Fact]
        public void Parse_ValidPhone_ReturnsPhoneInstance()
        {
            // Arrange
            var validPhone = "45988293345";

            // Act
            var phone = Phone.Parse(validPhone);

            // Assert
            phone.Value.Should().Be(validPhone);
        }

        [Fact]
        public void Parse_InvalidPhone_ThrowsBusinessException()
        {
            // Arrange
            var invalidPhone = "123";

            // Act
            Action act = () => Phone.Parse(invalidPhone);

            // Assert
            act.Should().Throw<BusinessException>();
        }

        [Fact]
        public void Parse_NullPhone_ThrowsArgumentNullException()
        {
            // Act
            Action act = () => Phone.Parse(null);

            // Assert
            act.Should().Throw<InvalidPhoneException>();
        }

        [Fact]
        public void IsValid_WithValidPhone_ReturnsValidResult()
        {
            // Arrange
            var validPhone = "45988293345";
            var phone = Phone.Parse(validPhone);

            // Act
            var result = phone.IsValid();

            // Assert
            result.IsValid.Should().BeTrue();
            result.Value.Should().Be(validPhone);
        }

        [Fact]
        public void ToString_ReturnsPhoneValue()
        {
            // Arrange
            var validPhone = "45988293345";
            var phone = Phone.Parse(validPhone);

            // Act
            var str = phone.ToString();

            // Assert
            str.Should().Be(validPhone);
        }

        [Fact]
        public void Empty_ReturnsDefaultPhone()
        {
            // Act
            var empty = Phone.Empty;

            // Assert
            empty.Should().Be("45988293345");
        }
    }
}
