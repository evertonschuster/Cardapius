using Administration.Application.Products.Commands.CreateProduct;


namespace Administration.Application.UnitTest.Products.Commands
{
    public class CreateProductHandlerTests
    {
        [Theory, CustomAutoData]
        public async Task Handle_ShouldCreateProductAndReturnSuccessResult(CreateProductCommand command)
        {
            // Arrange
            var productRepository = Substitute.For<IProductRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var sideDishes = new List<Product>();



            productRepository.ListWithAllPropertyByIds(Arg.Any<List<Guid>>()).Returns(sideDishes);
            productRepository.Create(Arg.Any<Product>()).Returns(x => x.Arg<Product>());

            var handler = new CreateProductHandler(productRepository, unitOfWork);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value!.Id.Should().NotBeEmpty();
            unitOfWork?.Received(1).Commit();
        }
    }
}
