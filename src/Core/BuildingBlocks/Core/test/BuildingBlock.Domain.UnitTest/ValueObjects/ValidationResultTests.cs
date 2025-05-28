using BuildingBlock.Domain.Exceptions;
using BuildingBlock.Domain.ValueObjects;
using System.Collections.ObjectModel;

namespace BuildingBlock.Domain.UnitTest.ValueObjects
{
    public class ValidationResultTests
    {
        [Fact]
        public void Success_Should_Create_Valid_Result()
        {
            // Arrange
            var value = "valid";

            // Act
            var result = ValidationResult<string>.Success(value);

            // Assert
            result.IsValid.Should().BeTrue();
            result.Value.Should().Be(value);
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void Failure_Should_Create_Invalid_Result_With_Errors()
        {
            // Arrange
            var value = "invalid";
            var exception = new BusinessException("Erro de negócio");
            var error = new ValidationError(exception);

            // Act
            var result = ValidationResult<string>.Failure(value, new[] { error });

            // Assert
            result.IsValid.Should().BeFalse();
            result.Value.Should().Be(value);
            result.Errors.Should().ContainSingle()
                .Which.Exception.Should().Be(exception);
        }

        [Fact]
        public void ThrowIfInvalid_Should_Throw_When_Result_Is_Invalid()
        {
            // Arrange
            var value = "invalid";
            var exception = new BusinessException("Erro de negócio");
            var error = new ValidationError(exception);
            var result = ValidationResult<string>.Failure(value, new[] { error });

            // Act
            Action act = () => result.ThrowIfInvalid();

            // Assert
            act.Should().Throw<BusinessException>().WithMessage("Erro de negócio");
        }

        [Fact]
        public void ThrowIfInvalid_Should_Not_Throw_When_Result_Is_Valid()
        {
            // Arrange
            var value = "valid";
            var result = ValidationResult<string>.Success(value);

            // Act
            Action act = () => result.ThrowIfInvalid();

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void ValidationError_Message_Should_Return_Exception_Message()
        {
            // Arrange
            var exception = new BusinessException("Mensagem de erro");
            var error = new ValidationError(exception);

            // Act & Assert
            error.Message.Should().Be("Mensagem de erro");
        }

        [Fact]
        public void ValidationResult_Success_Should_Be_Valid_And_Have_No_Errors()
        {
            // Act
            var result = ValidationResult.Success();

            // Assert
            result.IsValid.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            result.Errors.Should().BeEmpty();
            result.FirstError.Should().BeNull();
        }

        [Fact]
        public void ValidationResult_Constructor_Should_Set_Errors_As_ReadOnlyList()
        {
            // Arrange
            var error = ValidationError.FromMessage("Erro");
            var errors = new[] { error };

            // Act
            var result = (ValidationResult)Activator.CreateInstance(
                typeof(ValidationResult),
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance,
                null,
                new object[] { errors },
                null
            )!;

            // Assert
            result.Errors.Should().BeOfType<ReadOnlyCollection<ValidationError>>();
            result.Errors.Should().ContainSingle().Which.Should().Be(error);
        }

        [Fact]
        public void ValidationResult_FirstError_Should_Return_Null_When_No_Errors()
        {
            // Arrange
            var result = ValidationResult.Success();

            // Act & Assert
            result.FirstError.Should().BeNull();
        }

        [Fact]
        public void ValidationResult_FirstError_Should_Return_First_Error_When_Exists()
        {
            // Arrange
            var error1 = ValidationError.FromMessage("Erro 1");
            var error2 = ValidationError.FromMessage("Erro 2");

            // Act
            var result = ValidationResult.Failure(error1, error2);

            // Assert
            result.IsValid.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
            result.Errors.Should().HaveCount(2);
            result.FirstError.Should().Be(error1);
        }

        [Fact]
        public void ValidationResult_Failure_With_Enumerable_Should_Be_Invalid_And_Contain_Errors()
        {
            // Arrange
            var errors = new[] { ValidationError.FromMessage("Erro 1") };

            // Act
            var result = ValidationResult.Failure(errors);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().HaveCount(1);
            result.FirstError.Should().Be(errors[0]);
        }

        [Fact]
        public void GetMessageErrors_Should_Join_All_Error_Messages()
        {
            // Arrange
            var error1 = ValidationError.FromMessage("Erro 1");
            var error2 = ValidationError.FromMessage("Erro 2");
            var result = ValidationResult.Failure(error1, error2);

            // Act
            var message = result.GetMessageErrors();

            // Assert
            message.Should().Be("Erro 1\nErro 2");
        }

        [Fact]
        public void ThrowIfInvalid_Should_Throw_Default_Exception_When_FirstError_Is_Null()
        {
            // Act
            Action act = () =>
            {
                var result = ValidationResult.Failure();
                result.ThrowIfInvalid();
            };

            // Assert
            act.Should().Throw<BusinessException>();
        }
    }
}
