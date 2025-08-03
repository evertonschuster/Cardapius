using Administration.Application.Suppliers.Commands.CreateSupplier;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Administration.Api.Controllers;

[ApiController]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/suppliers")]
public class SupplierController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CreateSupplierCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
}
