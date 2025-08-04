using Administration.Application.Suppliers.Queries.ListSuppliers;

namespace Administration.Application.UnitTest.Suppliers.Queries;

public class ListSuppliersHandlerTests
{
    [Theory, CustomAutoData]
    public async Task Handle_ShouldReturnSuppliers(List<Supplier> suppliers)
    {
        var repository = Substitute.For<ISupplierRepository>();
        repository.ListAsync(CancellationToken.None).Returns(suppliers);

        var handler = new ListSuppliersHandler(repository);

        var result = await handler.Handle(new ListSuppliersQuery(), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(suppliers.Count);
    }
}

