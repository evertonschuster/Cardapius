using FluentValidation;

namespace Administration.Application.Suppliers.Commands.UpdateSupplier;

internal class UpdateSupplierValidator : AbstractValidator<UpdateSupplierCommand>
{
    public UpdateSupplierValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.LegalName).NotEmpty();
        RuleFor(x => x.Document).NotEmpty();
        RuleFor(x => x.PrimaryEmail).NotNull();
        RuleFor(x => x.Address).NotNull();
        RuleFor(x => x.BankInformation).NotNull();
    }
}

