using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sentinel.Api.Models;
using Sentinel.Api.ViewModels;

namespace Sentinel.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("register")]
    [Authorize(Roles = "SENTINEL.REGISTER_PASSWORD")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var user = new ApplicationUser { UserName = dto.Email, Email = dto.Email, IsActive = true };
        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);
        return Ok();
    }

    [HttpGet("login")]
    [AllowAnonymous]
    public IActionResult Login([FromQuery] string? returnUrl = null)
    {
        var form = "<form method='post'>" +
                   (string.IsNullOrEmpty(returnUrl) ? string.Empty : $"<input type='hidden' name='returnUrl' value='{returnUrl}' />") +
                   "<input type='email' name='Email'/>" +
                   "<input type='password' name='Password'/>" +
                   "<button type='submit'>Login</button></form>";

        var model = new LoginModel()
        {
            ReturnUrl = returnUrl,
        };
        return View(model);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromForm] LoginDto dto, [FromForm] string? returnUrl)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null)
            return Unauthorized();
        if (!user.IsActive || (user.AccessGrantedUntil.HasValue && user.AccessGrantedUntil < DateTime.UtcNow))
            return Unauthorized();
        var result = await _signInManager.PasswordSignInAsync(user, dto.Password, true, true);
        if (!result.Succeeded)
            return Unauthorized();
        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);
        return Ok();
    }

    [HttpPost("forgot-password")]
    [Authorize(Roles = "SENTINEL.FORGOT_PASSWORD")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null)
            return Ok();
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        return Ok(new { Token = token });
    }

    [HttpPost("reset-password")]
    [Authorize(Roles = "SENTINEL.RESET_PASSWORD")]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null)
            return BadRequest();
        var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
        if (!result.Succeeded)
            return BadRequest(result.Errors);
        return Ok();
    }

    [Authorize]
    [HttpPost("change-password")]
    [Authorize(Roles = "SENTINEL.CHANGE_PASSWORD")]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();
        var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
        if (!result.Succeeded)
            return BadRequest(result.Errors);
        return Ok();
    }
}
