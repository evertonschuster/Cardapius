using Administration.Domain.Suppliers.Repositories;

namespace Administration.Application.Suppliers.Commands.UpdateSupplier;

public class UpdateSupplierHandler(ISupplierRepository repository, IUnitOfWork unitOfWork) : ICommandHandler<UpdateSupplierCommand, UpdateSupplierResult>
{
    private readonly ISupplierRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<UpdateSupplierResult>> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = await _repository.GetByIdAsync(request.Id);
        if (supplier is null)
        {
            return Result<UpdateSupplierResult>.Fail(nameof(request.Id), $"Supplier with ID {request.Id} not found.");
        }

        supplier.Update(request.ToDto());

        await _repository.SaveAsync(supplier);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<UpdateSupplierResult>.Success(new UpdateSupplierResult());
    }
}

