using System;
using System.Collections.Generic;
using System.Linq;
using BuildingBlock.Domain.Exceptions;
using BuildingBlock.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

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
    }
}
