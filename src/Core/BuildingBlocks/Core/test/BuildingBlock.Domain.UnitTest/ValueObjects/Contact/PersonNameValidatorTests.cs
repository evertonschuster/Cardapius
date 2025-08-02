using BuildingBlock.Domain.ValueObjects.Contact;

namespace BuildingBlock.Domain.UnitTest.ValueObjects.Contact
{
    public class PersonNameValidatorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void Validate_Should_Fail_When_Name_Is_Null_Or_Empty(string? name)
        {
            var result = PersonNameValidator.Validate(name);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.Message == "O nome não pode estar vazio.");
        }

        [Theory]
        [InlineData("Ana S")]
        [InlineData("Jo S")]
        [InlineData("A B")]
        public void Validate_Should_Fail_When_Name_Is_Too_Short(string name)
        {
            var result = PersonNameValidator.Validate(name);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.Message == "O nome deve ter ao menos 6 caracteres.");
        }

        [Fact]
        public void Validate_Should_Fail_When_Name_Is_Too_Long()
        {
            var longName = new string('A', 101) + " Silva";
            var result = PersonNameValidator.Validate(longName);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.Message == "O nome deve ter no máximo 100 caracteres.");
        }

        [Theory]
        [InlineData("AnaAnaAnaAna")]
        [InlineData("JoãoJoão")]
        [InlineData("MariaMaria ")]
        public void Validate_Should_Fail_When_Missing_Surname(string name)
        {
            var result = PersonNameValidator.Validate(name);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.Message == "O nome deve conter pelo menos nome e sobrenome.");
        }

        [Theory]
        [InlineData("Ana123 Silva")]
        [InlineData("João@ Silva")]
        [InlineData("Maria! Souza")]
        [InlineData("Pedro#- Alves")]
        [InlineData("Lucas*' Souza")]
        public void Validate_Should_Fail_When_Name_Has_Invalid_Characters(string name)
        {
            var result = PersonNameValidator.Validate(name);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.Message == "O nome contém caracteres inválidos.");
        }

        [Theory]
        [InlineData("Ana Maria")]
        [InlineData("João da Silva")]
        [InlineData("Maria Souza")]
        [InlineData("José de Souza")]
        [InlineData("Luiz O'Connor")]
        [InlineData("Ana-Maria Silva")]
        [InlineData("João D'Ávila")]
        public void Validate_Should_Succeed_For_Valid_Names(string name)
        {
            var result = PersonNameValidator.Validate(name);

            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
        }
    }
}
