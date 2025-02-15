using LicenseClient.Models;

namespace LicenseClient
{
    class LicenseClient : ILicenseClient
    {
        public License Get()
        {
            return new License("ac3336d7-3a49-43b8-a1b9-4fef888aa8ea", DateTimeOffset.Now.AddDays(30));
        }
    }
}
