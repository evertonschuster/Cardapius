namespace BuildingBlock.Domain.ValueObjects.Business;

public readonly record struct Documentations(
    string? OperatingLicense,
    string? NegativeCertificates,
    string? AddressProof,
    string? SocialContract) : IValueObject
{
    public static Documentations Empty => new(null, null, null, null);

    public static Result<Documentations> Parse(
        string? operatingLicense,
        string? negativeCertificates,
        string? addressProof,
        string? socialContract)
    {
        var result = ValidationResult.Success();
        return Result<Documentations>.FromValidation(result,
            () => new Documentations(operatingLicense, negativeCertificates, addressProof, socialContract));
    }
}
