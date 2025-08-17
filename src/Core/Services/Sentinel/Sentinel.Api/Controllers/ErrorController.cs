using Microsoft.AspNetCore.Mvc;

namespace Sentinel.Api.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : Controller
{
    [Route("error")]
    public IActionResult Error()
    {
        return View("Index");
    }

    [Route("error/{statusCode:int}")]
    public IActionResult Error(int statusCode)
    {
        Response.StatusCode = statusCode;
        return View("Index", statusCode);
    }
}
