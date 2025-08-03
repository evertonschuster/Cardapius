using Administration.Domain.Suppliers.Repositories;

namespace Administration.Application.Suppliers.Commands.CreateSupplier;

internal class CreateSupplierValidator : AbstractValidator<CreateSupplierCommand>
{
    public CreateSupplierValidator(ISupplierRepository supplierRepository)
    {
        RuleFor(x => x.LegalName).NotEmpty();
        RuleFor(x => x.Document).NotEmpty();
        RuleFor(x => x.PrimaryEmail).NotNull();
        RuleFor(x => x.Address).NotNull();
        RuleFor(x => x.BankInformation).NotNull();
    }
}
