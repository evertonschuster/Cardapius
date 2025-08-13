namespace Sentinel.Api.ViewModels
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string? ReturnUrl { get; internal set; }
    }
}
