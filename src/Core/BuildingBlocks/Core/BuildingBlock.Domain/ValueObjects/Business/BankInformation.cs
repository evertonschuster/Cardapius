using System.Collections.Generic;
using System.Linq;

namespace BuildingBlock.Domain.ValueObjects.Business;

public readonly record struct BankInformation(
    string Bank,
    string Agency,
    string AccountNumber,
    AccountType AccountType,
    IReadOnlyList<string> PixKeys) : IValueObject
{
    public static BankInformation Empty => new("Banco", "0001", "12345-6", AccountType.Checking, new[] { "pix@teste.com" });

    public static Result<BankInformation> Parse(
        string? bank,
        string? agency,
        string? accountNumber,
        AccountType accountType,
        IEnumerable<string>? pixKeys)
    {
        var result = BankInformationValidator.Validate(bank, agency, accountNumber, pixKeys);
        return Result<BankInformation>.FromValidation(result,
            () => new BankInformation(bank!, agency!, accountNumber!, accountType, pixKeys!.ToList()));
    }
}

public enum AccountType
{
    Checking,
    Savings
}
