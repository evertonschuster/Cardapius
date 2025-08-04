using Administration.Domain.Suppliers.Repositories;

namespace Administration.Application.Suppliers.Commands.UpdateSupplier;

public class UpdateSupplierHandler(ISupplierRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateSupplierCommand, UpdateSupplierResult>
{
    private readonly ISupplierRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<UpdateSupplierResult> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = await _repository.GetByIdAsync(request.Id);
        if (supplier is null)
        {
            return new UpdateSupplierResult();
        }

        supplier.Update(request.ToDto());

        await _repository.SaveAsync(supplier);
        await _unitOfWork.CommitAsync();

        return new UpdateSupplierResult();
    }
}

