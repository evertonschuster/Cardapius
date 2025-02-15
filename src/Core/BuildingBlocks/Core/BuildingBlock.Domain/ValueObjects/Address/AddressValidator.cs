namespace BuildingBlock.Domain.ValueObjects.Address
{
    internal class AddressValidator
    {
        private AddressValidator()
        {
        }

        public static bool IsValid(string? street, string? number, string? complement, string? city, string? state, string? zipCode)
        {
            if (string.IsNullOrWhiteSpace(street) || string.IsNullOrWhiteSpace(number) || string.IsNullOrWhiteSpace(city) ||
                string.IsNullOrWhiteSpace(state) || string.IsNullOrWhiteSpace(zipCode))
            {
                return false;
            }

            return true;
        }
    }
}
