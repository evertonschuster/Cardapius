using System.Collections.Generic;
using System.Linq;

namespace BuildingBlock.Domain.ValueObjects.Business;

public class BankInformation : IValueObject, IValidatable
{
    public static BankInformation Empty => new()
    {
        Bank = "Banco",
        Agency = "0001",
        AccountNumber = "12345-6",
        AccountType = AccountType.Checking,
        PixKeys = ["pix@teste.com"]
    };

    public required string Bank { get; set; }
    public required string Agency { get; set; }
    public required string AccountNumber { get; set; }
    public AccountType AccountType { get; set; }
    public List<string> PixKeys { get; set; } = [];

    public static Result<BankInformation> Create(
        string? bank,
        string? agency,
        string? accountNumber,
        AccountType accountType,
        IEnumerable<string>? pixKeys)
    {
        var result = BankInformationValidator.Validate(bank, agency, accountNumber, pixKeys);
        return Result<BankInformation>.FromResult(result, () => new BankInformation()
        {
            Bank = bank!,
            Agency = agency!,
            AccountNumber = accountNumber!,
            AccountType = accountType,
            PixKeys = pixKeys!.ToList()
        });
    }

    public Result Validate()
    {
        return BankInformationValidator.Validate(Bank, Agency, AccountNumber, PixKeys);
    }
}

public enum AccountType
{
    Checking,
    Savings
}
