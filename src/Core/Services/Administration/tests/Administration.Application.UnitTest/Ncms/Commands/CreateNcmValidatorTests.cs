using Administration.Application.Ncms.Commands.CreateNcm;

namespace Administration.Application.UnitTest.Ncms.Commands
{
    public class CreateNcmValidatorTests
    {
        private readonly CreateNcmValidator _validator;

        public CreateNcmValidatorTests()
        {
            _validator = new CreateNcmValidator();
        }

        [Fact]
        public void Deve_Retornar_Erro_Quando_Descricao_Maior_Que_512()
        {
            var command = new CreateNcmCommand
            {
                Description = new string('a', 513)
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Description)
                .WithErrorMessage("Descrição deve ter no máximo 512 caracteres.");
        }

        [Fact]
        public void Nao_Deve_Retornar_Erro_Quando_Descricao_Menor_Ou_Igual_512()
        {
            var command = new CreateNcmCommand
            {
                Description = new string('a', 512)
            };

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }
    }
}