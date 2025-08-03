using BuildingBlock.Domain.ValueObjects;

namespace Administration.Domain.Suppliers.ValueObjects
{
    public class BankInformation : ValueObject
    {
        public string Bank { get; private set; }
        public string Agency { get; private set; }
        public string AccountNumber { get; private set; }
        public AccountType AccountType { get; private set; }
        public string PixKey { get; private set; }

        private BankInformation() { }

        public BankInformation(string bank, string agency, string accountNumber, AccountType accountType, string pixKey)
        {
            Bank = bank;
            Agency = agency;
            AccountNumber = accountNumber;
            AccountType = accountType;
            PixKey = pixKey;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Bank;
            yield return Agency;
            yield return AccountNumber;
            yield return AccountType;
            yield return PixKey;
        }
    }

    public enum AccountType
    {
        Checking,
        Savings
    }
}
