using LicenseClient.Models;

namespace LicenseClient
{
    //https://chatgpt.com/c/67afa77c-82d8-8000-a3d8-2d2a180eac73
    public interface ILicenseClient
    {
        License Get();
    }
}
