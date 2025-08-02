using Administration.Application.Restaurants.Commands.CreateRestaurant;
using Administration.Domain.Restaurants.Models;
using Administration.Domain.Restaurants.Repositories;

namespace Administration.Application.UnitTest.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantHandlerTests
    {
        [Theory, CustomAutoData]
        public async Task Handle_DeveSalvarRestauranteECommitarTransacao(CreateRestaurantCommand command)
        {
            // Arrange
            var restaurantRepository = Substitute.For<IRestaurantRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();

            var handler = new CreateRestaurantHandler(restaurantRepository, unitOfWork);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            await restaurantRepository.Received(1).SaveAsync(Arg.Any<Restaurant>());
            await unitOfWork.Received(1).CommitAsync();
            result.Should().NotBeNull();
        }
    }
}
