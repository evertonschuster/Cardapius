namespace BuildingBlock.Domain.ValueObjects.Business;

internal static class CpfCnpjValidator
{
    private const int CpfLength = 11;
    private const int CnpjLength = 14;

    public static ValidationResult Validate(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return ValidationResult.Failure("CpfCnpj", "O documento não pode estar vazio.");

        var digits = new string(value.Where(char.IsDigit).ToArray());
        if (digits.Length != CpfLength && digits.Length != CnpjLength)
            return ValidationResult.Failure("CpfCnpj", $"O documento deve ter {CpfLength} ou {CnpjLength} dígitos.");

        return ValidationResult.Success();
    }
}