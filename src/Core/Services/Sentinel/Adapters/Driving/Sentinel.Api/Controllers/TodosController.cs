using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sentinel.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private static readonly List<string> Todos = new();

    [HttpGet]
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme, Policy = "ApiScope")]
    public IActionResult Get() => Ok(Todos);

    [HttpPost]
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme, Roles = "writer", Policy = "ApiScope")]
    public IActionResult Post([FromBody] string todo)
    {
        Todos.Add(todo);
        return Ok();
    }
}
