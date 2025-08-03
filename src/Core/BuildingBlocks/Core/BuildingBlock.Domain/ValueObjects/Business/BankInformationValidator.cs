namespace BuildingBlock.Domain.ValueObjects.Business;

internal static class BankInformationValidator
{
    public static ValidationResult Validate(string? bank, string? agency, string? accountNumber, string? pixKey)
    {
        if (string.IsNullOrWhiteSpace(bank))
            return ValidationResult.Failure("Bank", "O banco não pode estar vazio.");
        if (string.IsNullOrWhiteSpace(agency))
            return ValidationResult.Failure("Agency", "A agência não pode estar vazia.");
        if (string.IsNullOrWhiteSpace(accountNumber))
            return ValidationResult.Failure("AccountNumber", "A conta não pode estar vazia.");
        if (string.IsNullOrWhiteSpace(pixKey))
            return ValidationResult.Failure("PixKey", "A chave PIX não pode estar vazia.");
        return ValidationResult.Success();
    }
}
