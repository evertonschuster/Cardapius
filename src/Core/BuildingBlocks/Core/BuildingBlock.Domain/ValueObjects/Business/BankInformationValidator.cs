using System.Collections.Generic;
using System.Linq;

namespace BuildingBlock.Domain.ValueObjects.Business;

    internal static class BankInformationValidator
    {
        public static ValidationResult Validate(string? bank, string? agency, string? accountNumber, IEnumerable<string>? pixKeys)
        {
            if (string.IsNullOrWhiteSpace(bank))
                return ValidationResult.Failure("Bank", "O banco não pode estar vazio.");
            if (string.IsNullOrWhiteSpace(agency))
                return ValidationResult.Failure("Agency", "A agência não pode estar vazia.");
            if (string.IsNullOrWhiteSpace(accountNumber))
                return ValidationResult.Failure("AccountNumber", "A conta não pode estar vazia.");
            if (pixKeys is null || !pixKeys.Any() || pixKeys.Any(string.IsNullOrWhiteSpace))
                return ValidationResult.Failure("PixKeys", "As chaves PIX não podem estar vazias.");
            return ValidationResult.Success();
        }
    }
