namespace BuildingBlock.Domain.ValueObjects.Business;

public readonly record struct BankInformation(
    string Bank,
    string Agency,
    string AccountNumber,
    AccountType AccountType,
    string PixKey) : IValueObject
{
    public static BankInformation Empty => new("Banco", "0001", "12345-6", AccountType.Checking, "pix@teste.com");

    public static Result<BankInformation> Parse(
        string? bank,
        string? agency,
        string? accountNumber,
        AccountType accountType,
        string? pixKey)
    {
        var result = BankInformationValidator.Validate(bank, agency, accountNumber, pixKey);
        return Result<BankInformation>.FromValidation(result,
            () => new BankInformation(bank!, agency!, accountNumber!, accountType, pixKey!));
    }
}

public enum AccountType
{
    Checking,
    Savings
}
