namespace BuildingBlock.Domain.ValueObjects.Business;

internal static class MunicipalRegistrationValidator
{
    public static ValidationResult Validate(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return ValidationResult.Failure("MunicipalRegistration", "A inscrição municipal não pode estar vazia.");
        return ValidationResult.Success();
    }
}
