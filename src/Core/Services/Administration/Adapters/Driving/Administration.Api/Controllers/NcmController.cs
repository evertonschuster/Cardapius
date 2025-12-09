using Administration.Application.Ncms.Commands.CreateNcm;
using Administration.Application.Ncms.Commands.UpdateNcm;
using Administration.Application.Ncms.Queries.GetNcmById;
using Administration.Application.Ncms.Queries.ListNcms;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Administration.Api.Controllers;

[ApiController]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/ncms")]
public class NcmController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CreateNcmCommand command)
    {
        var result = await mediator.Send(command);
        if (result.IsSuccess)
        {
            return Ok(result);
        }

        return BadRequest(result.Errors);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] UpdateNcmCommand command)
    {
        command.Id = id;
        var result = await mediator.Send(command);
        if (result.IsSuccess)
        {
            return Ok(result);
        }

        return BadRequest(result.Errors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var result = await mediator.Send(new GetNcmByIdQuery(id));
        if (result.IsSuccess)
        {
            return Ok(result);
        }

        return BadRequest(result.Errors);
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] ListNcmsQuery query)
    {
        var result = await mediator.Send(query);
        if (result.IsSuccess)
        {
            return Ok(result);
        }

        return BadRequest(result.Errors);
    }
}
