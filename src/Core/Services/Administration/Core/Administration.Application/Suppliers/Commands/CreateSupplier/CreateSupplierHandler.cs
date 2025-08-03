using Administration.Domain.Suppliers.Repositories;

namespace Administration.Application.Suppliers.Commands.CreateSupplier;

public class CreateSupplierHandler(ISupplierRepository supplierRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreateSupplierCommand, CreateSupplierResult>
{
    private readonly ISupplierRepository _supplierRepository = supplierRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<CreateSupplierResult>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
    {
        var model = request.ToModel();
        await _supplierRepository.SaveAsync(model, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        return Result<CreateSupplierResult>.Success(new CreateSupplierResult());
    }
}
