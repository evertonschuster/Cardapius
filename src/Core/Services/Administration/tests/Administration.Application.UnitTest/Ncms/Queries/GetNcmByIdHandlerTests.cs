using Administration.Application.Ncms.Queries.GetNcmById;
using Administration.Domain.Products.Entities;
using NSubstitute;

namespace Administration.Application.UnitTest.Ncms.Queries
{
    public class GetNcmByIdHandlerTests
    {
        private readonly INcmRepository _repositorySubstitute;
        private readonly GetNcmByIdHandler _handler;

        public GetNcmByIdHandlerTests()
        {
            _repositorySubstitute = Substitute.For<INcmRepository>();
            _handler = new GetNcmByIdHandler(_repositorySubstitute);
        }

        [Fact]
        public async Task Handle_DeveRetornarFalha_QuandoNcmNaoEncontrada()
        {
            // Arrange
            var query = new GetNcmByIdQuery(Guid.NewGuid());

            _repositorySubstitute.GetByIdAsync(query.Id).Returns((Ncm?)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().ContainSingle()
                .Which.PropertyName.Should().Be(nameof(query.Id));
        }

        [Theory, CustomAutoData]
        public async Task Handle_DeveRetornarSucesso_QuandoNcmEncontrada(Ncm ncmModel)
        {
            // Arrange
            var query = new GetNcmByIdQuery(Guid.NewGuid());

            _repositorySubstitute.GetByIdAsync(query.Id).Returns(ncmModel);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            await _repositorySubstitute.Received(1).GetByIdAsync(query.Id);
        }
    }
}