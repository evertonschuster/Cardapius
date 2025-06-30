using Administration.Application.Products.Commands.CreateProduct;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Administration.Api.Controllers;

[ApiController]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/products")]
public class ProductController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CreateProductCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
}
