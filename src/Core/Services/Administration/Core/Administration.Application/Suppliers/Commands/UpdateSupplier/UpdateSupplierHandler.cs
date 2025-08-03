using Administration.Domain.Suppliers.Repositories;
using BuildingBlock.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

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

        supplier.Update(
            request.LegalName,
            request.TradeName,
            request.Document,
            request.StateRegistration,
            request.MunicipalRegistration,
            request.PersonType,
            request.RegistrationDate,
            request.Status,
            request.RepresentativeName,
            request.LandlinePhone,
            request.MobilePhone,
            request.PrimaryEmail,
            request.SecondaryEmail,
            request.Website,
            request.Address,
            request.BankInformation,
            request.Category,
            request.PaymentTerms,
            request.DeliveryTime,
            request.ShippingMethod,
            request.OfferedProductsServices,
            request.Documentations,
            request.AdditionalNotes,
            request.RelationshipHistory);

        await _repository.SaveAsync(supplier);
        await _unitOfWork.CommitAsync();

        return new UpdateSupplierResult();
    }
}

