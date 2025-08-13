using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sentinel.Api.Pages;

public class LoginModel : PageModel
{
    [BindProperty]
    [Required]
    public string Username { get; set; } = "administrador";

    [BindProperty]
    [Required]
    public string Password { get; set; } = string.Empty;

    public void OnGet()
    {
    }

    public void OnPost()
    {
        if (ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Credenciais inválidas (demo)");
        }
    }
}
