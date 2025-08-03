using Administration.Application.Suppliers.Commands.CreateSupplier;
using Administration.Application.Suppliers.Commands.UpdateSupplier;
using Administration.Application.Suppliers.Queries.ListSuppliers;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

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

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] UpdateSupplierCommand command)
    {
        command.Id = id;
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var result = await mediator.Send(new ListSuppliersQuery());
        return Ok(result);
    }
}
