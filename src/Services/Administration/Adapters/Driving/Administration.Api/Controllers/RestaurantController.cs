using Administration.Application.Restaurants.Commands.CreateRestaurant;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Administration.Api.Controllers
{
    [ApiController]
    [ApiVersion(1.0)]
    [Route("[module]/api/v{version:apiVersion}/restaurants")]
    public class RestaurantController : ControllerBase
    {
        public RestaurantController(IMediator mediator)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public IMediator Mediator { get; private set; }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateRestaurantCommand command)
        {
            var result = await this.Mediator.Send(command);
            return Ok(result);
        }
    }
}
