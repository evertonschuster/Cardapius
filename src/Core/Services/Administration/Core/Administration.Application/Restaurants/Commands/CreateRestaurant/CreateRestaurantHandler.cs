using Administration.Domain.Restaurants.Repositories;

namespace Administration.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantHandler(IRestaurantRepository restaurantRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateRestaurantCommand, CreateRestaurantResult>
    {
        private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CreateRestaurantResult> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var model = request.ToModel();

            await _restaurantRepository.SaveAsync(model);
            await _unitOfWork.CommitAsync();

            var result = new CreateRestaurantResult();
            return result;
        }
    }
}
