using Administration.Domain.Suppliers.Repositories;

namespace Administration.Application.Suppliers.Commands.CreateSupplier;

public class CreateSupplierHandler(ISupplierRepository supplierRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateSupplierCommand, CreateSupplierResult>
{
    private readonly ISupplierRepository _supplierRepository = supplierRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<CreateSupplierResult> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
    {
        var model = request.ToModel();
        await _supplierRepository.SaveAsync(model);
        await _unitOfWork.CommitAsync();
        return new CreateSupplierResult();
    }
}
