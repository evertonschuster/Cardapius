using System.Threading;
using System.Threading.Tasks;
using Administration.Application.Restaurants.Commands.CreateRestaurant;
using Administration.Domain.Restaurants.Repositories;
using NSubstitute;
using Xunit;
using FluentAssertions;
using Administration.Domain.Restaurants.Models;

namespace Administration.Application.UnitTest.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantHandlerTests
    {
        [Fact]
        public async Task Handle_DeveSalvarRestauranteECommitarTransacao()
        {
            // Arrange
            var restaurantRepository = Substitute.For<IRestaurantRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var command = Substitute.For<CreateRestaurantCommand>();
            var model = Substitute.For<Restaurant>();
            command.ToModel().Returns(model);

            var handler = new CreateRestaurantHandler(restaurantRepository, unitOfWork);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            await restaurantRepository.Received(1).SaveAsync(model);
            await unitOfWork.Received(1).CommitAsync();
            result.Should().NotBeNull();
        }
    }
}
