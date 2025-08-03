using BuildingBlock.Domain.ValueObjects;

namespace Administration.Domain.Suppliers.ValueObjects
{
    public class Documentations : ValueObject
    {
        public string? OperatingLicense { get; private set; }
        public string? NegativeCertificates { get; private set; }
        public string? AddressProof { get; private set; }
        public string? SocialContract { get; private set; }

        private Documentations() { }

        public Documentations(string? operatingLicense, string? negativeCertificates, string? addressProof, string? socialContract)
        {
            OperatingLicense = operatingLicense;
            NegativeCertificates = negativeCertificates;
            AddressProof = addressProof;
            SocialContract = socialContract;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return OperatingLicense;
            yield return NegativeCertificates;
            yield return AddressProof;
            yield return SocialContract;
        }
    }
}
