using Administration.Domain.Restaurants.Repositories;

namespace Administration.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantHandler : IRequestHandler<CreateRestaurantCommand, CreateRestaurantResult>
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public CreateRestaurantHandler(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
        }

        public Task<CreateRestaurantResult> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var model = request.ToModel();
            _restaurantRepository.Save(model);

            return Task.FromResult(new CreateRestaurantResult());
        }
    }
}
