namespace BuildingBlock.Domain.ValueObjects.Business;

internal static class DocumentValidator
{
    private static readonly string[] ValidLengths = ["11", "14"]; // Accept CPF (11) or CNPJ (14)

    public static ValidationResult Validate(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return ValidationResult.Failure("Document", "O documento não pode estar vazio.");

        var digits = new string(value.Where(char.IsDigit).ToArray());
        if (digits.Length != 11 && digits.Length != 14)
            return ValidationResult.Failure("Document", "O documento deve ter 11 ou 14 dígitos.");

        return ValidationResult.Success();
    }
}
