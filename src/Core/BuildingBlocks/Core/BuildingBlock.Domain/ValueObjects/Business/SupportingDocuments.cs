namespace BuildingBlock.Domain.ValueObjects.Business;

public readonly record struct SupportingDocuments(
    string? OperatingLicense,
    string? NegativeCertificates,
    string? AddressProof,
    string? SocialContract) : IValueObject
{
    public static SupportingDocuments Empty => new(null, null, null, null);

    public static Result<SupportingDocuments> Parse(
        string? operatingLicense,
        string? negativeCertificates,
        string? addressProof,
        string? socialContract)
    {
        var result = ValidationResult.Success();
        return Result<SupportingDocuments>.FromValidation(result,
            () => new SupportingDocuments(operatingLicense, negativeCertificates, addressProof, socialContract));
    }
}
