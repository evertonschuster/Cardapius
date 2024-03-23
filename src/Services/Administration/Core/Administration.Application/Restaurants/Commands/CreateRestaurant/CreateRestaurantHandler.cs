using Administration.Domain.Restaurants.Repositories;
using BuildingBlock.Domain;

namespace Administration.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantHandler : IRequestHandler<CreateRestaurantCommand, CreateRestaurantResult>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRestaurantHandler(IRestaurantRepository restaurantRepository, IUnitOfWork unitOfWork)
        {
            _restaurantRepository = restaurantRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateRestaurantResult> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var model = request.ToModel();

            await _restaurantRepository.SaveAsync(model);
            _unitOfWork.Commit();

            var result = new CreateRestaurantResult();
            return result;
        }
    }
}
