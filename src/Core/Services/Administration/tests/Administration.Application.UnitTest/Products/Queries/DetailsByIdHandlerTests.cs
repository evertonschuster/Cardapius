using Administration.Application.Products.Queries.DetailsById;

namespace Administration.Application.UnitTest.Products.Queries.DetailsById
{
    public class DetailsByIdHandlerTests
    {
        private readonly IProductRepository _productRepository;
        private readonly DetailsByIdHandler _handler;

        public DetailsByIdHandlerTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _handler = new DetailsByIdHandler(_productRepository);
        }

        [Fact]
        public async Task Handle_ShouldReturnFail_WhenProductNotFound()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _productRepository.GetWithAllPropertyByIds(productId).Returns((Product?)null);
            var query = new DetailsByIdQuery(productId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().NotBeNullOrEmpty();
            result.Errors[0].PropertyName.Should().Be(nameof(DetailsByIdQuery.Id));
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenProductFound()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = Substitute.For<Product>();
            _productRepository.GetWithAllPropertyByIds(productId).Returns(product);
            var query = new DetailsByIdQuery(productId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Id.Should().Be(product.Id);
        }
    }
}
