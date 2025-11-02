using BuildingBlock.Domain.ValueObjects.Business;

namespace BuildingBlock.Domain.UnitTest.ValueObjects.Business
{
    public class SupportingDocumentsTests
    {
        [Fact]
        public void Empty_DeveRetornarTodosOsCamposNulos()
        {
            var empty = SupportingDocuments.Empty;
            empty.OperatingLicense.Should().BeNull();
            empty.NegativeCertificates.Should().BeNull();
            empty.AddressProof.Should().BeNull();
            empty.SocialContract.Should().BeNull();
        }

        [Fact]
        public void Parse_DeveRetornarObjetoComValoresCorretos()
        {
            var result = SupportingDocuments.Parse("Licença", "Certificados", "Comprovante", "Contrato");
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value!.OperatingLicense.Should().Be("Licença");
            result.Value.NegativeCertificates.Should().Be("Certificados");
            result.Value.AddressProof.Should().Be("Comprovante");
            result.Value.SocialContract.Should().Be("Contrato");
        }

        [Fact]
        public void Validate_DeveRetornarSucesso_QuandoTodosOsCamposSaoValidosOuNulos()
        {
            var docs = new SupportingDocuments
            {
                OperatingLicense = "Licença",
                NegativeCertificates = "Certificados",
                AddressProof = "Comprovante",
                SocialContract = "Contrato"
            };
            var result = docs.Validate();
            result.IsSuccess.Should().BeTrue();

            var emptyDocs = SupportingDocuments.Empty;
            var resultEmpty = emptyDocs.Validate();
            resultEmpty.IsSuccess.Should().BeTrue();
        }
    }
}