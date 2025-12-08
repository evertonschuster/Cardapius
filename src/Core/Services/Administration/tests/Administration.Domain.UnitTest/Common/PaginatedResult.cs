using Administration.Domain.Common.Pagination;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Administration.Domain.UnitTest.Common
{
    public class PaginatedResultTests
    {
        [Fact]
        public void Constructor_DeveAtribuirPropriedadesCorretamente()
        {
            var items = new List<string> { "A", "B" };
            var pageNumber = 2;
            var pageSize = 5;
            var totalCount = 12;

            var result = new PaginatedResult<string>(items, pageNumber, pageSize, totalCount);

            result.Items.Should().BeEquivalentTo(items);
            result.PageNumber.Should().Be(pageNumber);
            result.PageSize.Should().Be(pageSize);
            result.TotalCount.Should().Be(totalCount);
        }

        [Theory]
        [InlineData(10, 3, 4)]
        [InlineData(9, 3, 3)]
        [InlineData(0, 5, 0)]
        [InlineData(1, 1, 1)]
        public void TotalPages_DeveCalcularCorretamente(int totalCount, int pageSize, int expectedTotalPages)
        {
            var result = new PaginatedResult<int>(new List<int>(), 1, pageSize, totalCount);

            result.TotalPages.Should().Be(expectedTotalPages);
        }
    }
}