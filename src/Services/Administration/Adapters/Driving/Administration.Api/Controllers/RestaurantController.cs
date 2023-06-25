using Administration.Application.Restaurants.Commands.CreateRestaurant;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Administration.Api.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
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
