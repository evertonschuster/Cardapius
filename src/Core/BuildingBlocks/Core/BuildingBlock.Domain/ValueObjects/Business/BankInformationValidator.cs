using System.Collections.Generic;
using System.Linq;

namespace BuildingBlock.Domain.ValueObjects.Business;

internal static class BankInformationValidator
{
    public static Result Validate(string? bank, string? agency, string? accountNumber, IEnumerable<string>? pixKeys)
    {
        if (string.IsNullOrWhiteSpace(bank))
            return Result.Fail("Bank", "O banco não pode estar vazio.");

        if (string.IsNullOrWhiteSpace(agency))
            return Result.Fail("Agency", "A agência não pode estar vazia.");

        if (string.IsNullOrWhiteSpace(accountNumber))
            return Result.Fail("AccountNumber", "A conta não pode estar vazia.");

        if (pixKeys is null || !pixKeys.Any() || pixKeys.Any(string.IsNullOrWhiteSpace))
            return Result.Fail("PixKeys", "As chaves PIX não podem estar vazias.");

        return Result.Success();
    }
}
