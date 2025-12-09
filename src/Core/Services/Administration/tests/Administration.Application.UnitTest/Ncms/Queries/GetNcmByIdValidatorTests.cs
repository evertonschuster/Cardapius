using Administration.Application.Ncms.Queries.GetNcmById;

namespace Administration.Application.UnitTest.Ncms.Queries
{
    public class GetNcmByIdValidatorTests
    {
        private readonly GetNcmByIdValidator _validator;

        public GetNcmByIdValidatorTests()
        {
            _validator = new GetNcmByIdValidator();
        }

        [Fact]
        public void Nao_Deve_Retornar_Erro_Quando_Id_For_Preenchido()
        {
            var query = new GetNcmByIdQuery(Guid.NewGuid());

            var result = _validator.TestValidate(query);

            result.ShouldNotHaveValidationErrorFor(q => q.Id);
        }
    }
}