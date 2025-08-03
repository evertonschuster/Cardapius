using Administration.Application.Suppliers.Commands.UpdateSupplier;

namespace Administration.Application.UnitTest.Suppliers.Commands;

public class UpdateSupplierHandlerTests
{
    [Theory, CustomAutoData]
    public async Task Handle_ShouldUpdateSupplierAndCommit(UpdateSupplierCommand command, Supplier supplier)
    {
        supplier = supplier ?? throw new ArgumentNullException(nameof(supplier));
        command.Id = supplier.Id;

        var repository = Substitute.For<ISupplierRepository>();
        repository.GetByIdAsync(command.Id).Returns(supplier);
        var unitOfWork = Substitute.For<IUnitOfWork>();

        var handler = new UpdateSupplierHandler(repository, unitOfWork);

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        await repository.Received(1).SaveAsync(supplier);
        await unitOfWork.Received(1).CommitAsync();
    }
}

