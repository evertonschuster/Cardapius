using Administration.Application.Suppliers.Commands.CreateSupplier;

namespace Administration.Application.UnitTest.Suppliers.Commands;

public class CreateSupplierHandlerTests
{
    [Theory, CustomAutoData]
    public async Task Handle_ShouldCreateSupplierAndCommit(CreateSupplierCommand command)
    {
        var repository = Substitute.For<ISupplierRepository>();
        var unitOfWork = Substitute.For<IUnitOfWork>();
        var handler = new CreateSupplierHandler(repository, unitOfWork);

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        await repository.Received(1).SaveAsync(Arg.Any<Supplier>());
        await unitOfWork.Received(1).CommitAsync();
    }
}
