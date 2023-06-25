namespace BuildingBlock.Domain.ValueObjects.PersonNames
{
    internal static class PersonNameValidator
    {
        public static bool IsValid(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            if (name.Length <= 5)
            {
                return false;
            }

            return name
                .Split(" ")
                .Length >= 2;
        }
    }
}
