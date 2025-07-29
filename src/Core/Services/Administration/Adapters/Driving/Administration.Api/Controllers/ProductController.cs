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

    /// <summary>
    /// Retrieves the details of a product by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the product to retrieve.</param>
    /// <returns>An HTTP 200 OK response containing the product details.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id)
    {
        var query = new DetailsByIdQuery(id);
        var result = await mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Handles HTTP POST requests to create a new product.
    /// </summary>
    /// <param name="command">The command containing product creation details.</param>
    /// <returns>An HTTP 200 OK response with the result of the product creation.</returns>
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CreateProductCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
}
