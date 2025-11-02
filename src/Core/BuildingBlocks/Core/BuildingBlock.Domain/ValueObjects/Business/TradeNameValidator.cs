namespace BuildingBlock.Domain.ValueObjects.Business;

internal static class TradeNameValidator
{
    private const int MinLength = 2;
    private const int MaxLength = 150;

    public static ValidationResult Validate(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return ValidationResult.Failure("TradeName", "O nome fantasia não pode estar vazio.");

        var trimmed = value.Trim();
        if (trimmed.Length < MinLength)
            return ValidationResult.Failure("TradeName", $"O nome fantasia deve ter ao menos {MinLength} caracteres.");
        if (trimmed.Length > MaxLength)
            return ValidationResult.Failure("TradeName", $"O nome fantasia deve ter no máximo {MaxLength} caracteres.");
        return ValidationResult.Success();
    }
}
