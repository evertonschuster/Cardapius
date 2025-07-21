using Administration.Application.Products.Commands.CreateProduct;
using Administration.Application.Products.Queries.DetailsById;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Administration.Api.Controllers;

[ApiController]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/products")]
public class ProductController(IMediator mediator) : ControllerBase
{

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id)
    {
        var query = new DetailsByIdQuery(id);
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CreateProductCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
}
