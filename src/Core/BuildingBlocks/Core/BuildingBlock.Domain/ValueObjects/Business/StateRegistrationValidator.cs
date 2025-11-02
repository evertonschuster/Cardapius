namespace BuildingBlock.Domain.ValueObjects.Business;

internal static class StateRegistrationValidator
{
    public static ValidationResult Validate(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return ValidationResult.Failure("StateRegistration", "A inscrição estadual não pode estar vazia.");
        return ValidationResult.Success();
    }
}
