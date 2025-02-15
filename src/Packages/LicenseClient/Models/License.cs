namespace LicenseClient.Models
{
    public record License
    {
        public License(string key, DateTimeOffset expireAt)
        {
            Key = key;
            ExpireAt = expireAt;
        }

        public bool IsValid
        {
            get => DateTimeOffset.Now < ExpireAt;

        }

        public DateTimeOffset ExpireAt { get; }

        public string Key { get; }

        internal static License Create(string key, DateTimeOffset expireAt)
        {
            return new License(key, expireAt);
        }

    }
}
