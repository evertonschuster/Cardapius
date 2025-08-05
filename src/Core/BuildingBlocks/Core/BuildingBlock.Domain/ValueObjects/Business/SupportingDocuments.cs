namespace BuildingBlock.Domain.ValueObjects.Business;

public class SupportingDocuments : IValueObject, IValidatable
{
    public static SupportingDocuments Empty => new()
    {
        OperatingLicense = null,
        NegativeCertificates = null,
        AddressProof = null,
        SocialContract = null
    };

    public string? OperatingLicense { get; set; }
    public string? NegativeCertificates { get; set; }
    public string? AddressProof { get; set; }
    public string? SocialContract { get; set; }


    public static Result<SupportingDocuments> Parse(
        string? operatingLicense,
        string? negativeCertificates,
        string? addressProof,
        string? socialContract)
    {
        return Result<SupportingDocuments>.Success(new SupportingDocuments()
        {
            OperatingLicense = operatingLicense,
            NegativeCertificates = negativeCertificates,
            AddressProof = addressProof,
            SocialContract = socialContract
        });
    }

    public Result Validate()
    {
        return Result.Success();
    }
}
