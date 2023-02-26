using BuildingBlock.Domain.ValueObject.Email.Exceptions;
using BuildingBlock.Domain.ValueObjects.Emails;

namespace BuildingBlock.Domain.UnitTest.ValueObjects
{
    public class EmailTests
    {
        [Theory]
        [InlineData("matheus@gmail.com")]
        [InlineData("francisco.silva@hotmail.com")]
        [InlineData("marcos_cuimbra09@outlook.com")]
        [InlineData("joao.teste@minhaempresa.com.br")]
        public void CreateWithValidEmail_Success(string emailText)
        {
            //Arrange

            //Act
            var email = Email.Parse(emailText);


            //Asserts
            //email.Value
            //    .Should()
            //    .Be(email);
        }

        [Theory]
        [InlineData("mat heus@gmail.com")]
        [InlineData("francisco.silva@hotma il.com")]
        [InlineData(" marcos_cuimbra09@outlook.com")]
        [InlineData("joao.teste@minhaempresa.com.br ")]
        public void CreateWithWhiteSpace_Fail(string emailText)
        {
            //Arrange

            //Act
            Action act = () => Email.Parse(emailText);


            //Asserts
            act.Should()
                .Throw<InvalidEmailException>();
        }

        [Theory]
        [InlineData("matheus@")]
        [InlineData("francisco.silva")]
        [InlineData("marcos_cuimbra09@outlook")]
        public void CreateWithoutEmailProvider_Fail(string emailText)
        {
            //Arrange

            //Act
            Action act = () => Email.Parse(emailText);


            //Asserts
            act.Should()
                .Throw<InvalidEmailException>();
        }

        [Theory]
        [InlineData("joao.testeminhaempresa.com.br")]
        [InlineData("joao.testeminhaempresacombr")]
        public void CreateWithInvalidEmail_Fail(string emailText)
        {
            //Arrange

            //Act
            Action act = () => Email.Parse(emailText);


            //Asserts
            act.Should()
                .Throw<InvalidEmailException>();
        }
    }
}
