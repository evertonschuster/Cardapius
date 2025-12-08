using Administration.Application.Ncms.Queries.ListNcms;
using Administration.Domain.Common.Pagination;
using Administration.Domain.Ncms.ValueObjects;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace Administration.Application.UnitTest.Ncms.Queries
{
    public class ListNcmsHandlerTests
    {
        private readonly INcmRepository _repository;
        private readonly ListNcmsHandler _handler;

        public ListNcmsHandlerTests()
        {
            _repository = Substitute.For<INcmRepository>();
            _handler = new ListNcmsHandler(_repository);
        }

        [Fact]
        public async Task Handle_DeveRetornarItensPaginadosMapeados()
        {
            // Arrange
            var query = new ListNcmsQuery { PageNumber = 1, PageSize = 2 };
            var ncmModels = new[] { Substitute.For<Ncm>(), Substitute.For<Ncm>() };
            var pagedResult = new PaginatedResult<Ncm>(ncmModels, 1, 2, 10);

            _repository.ListAsync(1, 2, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(pagedResult));

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value!.Items.Should().HaveCount(2);
            result.Value.PageNumber.Should().Be(1);
            result.Value.PageSize.Should().Be(2);
            result.Value.TotalCount.Should().Be(10);
        }

        [Fact]
        public async Task Handle_DeveRetornarListaVazia_QuandoNaoExistemItens()
        {
            // Arrange
            var query = new ListNcmsQuery { PageNumber = 1, PageSize = 2 };
            var pagedResult = new PaginatedResult<Ncm>([], 1, 2, 0);

            _repository.ListAsync(1, 2, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(pagedResult));

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value!.Items.Should().BeEmpty();
            result.Value.TotalCount.Should().Be(0);
        }
    }
}