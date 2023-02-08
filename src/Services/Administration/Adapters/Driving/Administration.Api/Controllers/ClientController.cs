using Administration.Application.Clients.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Administration.Api.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] CreateClientCommand command)
        {
            return Ok(command);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateClient([FromBody] CreateClientCommand command)
        {
            return Ok(command);
        }
    }
}
