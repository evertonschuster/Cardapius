using System.ComponentModel.DataAnnotations;

namespace Sentinel.Api.ViewModels;

public class LoginModel
{
    [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo E-mail não é um endereço de e-mail válido.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Senha é obrigatório.")]
    public string Password { get; set; } = string.Empty;

    public string? ReturnUrl { get; set; }

    public bool PasswordVisible { get; set; }
}