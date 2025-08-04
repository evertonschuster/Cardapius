using Administration.Domain.Suppliers;
using BuildingBlock.Domain.ValueObjects.Business;

namespace Administration.Application.Suppliers.Commands.UpdateSupplier;

public class UpdateSupplierCommand : ICommandRequest<UpdateSupplierResult>
{
    public Guid Id { get; set; }

    public LegalName LegalName { get; set; }
    public TradeName TradeName { get; set; }
    public CpfCnpj CpfCnpj { get; set; }
    public StateRegistration StateRegistration { get; set; }
    public MunicipalRegistration MunicipalRegistration { get; set; }
    public PersonType PersonType { get; set; }
    public DateTime RegistrationDate { get; set; }
    public SupplierStatus Status { get; set; }

    public PersonName RepresentativeName { get; set; }
    public Phone LandlinePhone { get; set; }
    public Phone MobilePhone { get; set; }
    public Email PrimaryEmail { get; set; }
    public Email SecondaryEmail { get; set; }
    public string? Website { get; set; }

    public required Address Address { get; set; }
    public required BankInformation BankInformation { get; set; }

    public string Category { get; set; } = string.Empty;
    public string PaymentTerms { get; set; } = string.Empty;
    public string DeliveryTime { get; set; } = string.Empty;
    public string ShippingMethod { get; set; } = string.Empty;
    public string OfferedProductsServices { get; set; } = string.Empty;

    public SupportingDocuments? SupportingDocuments { get; set; }
    public string? AdditionalNotes { get; set; }
    public string? RelationshipHistory { get; set; }
    internal SupplierDto ToDto() => new(
        LegalName,
        TradeName,
        CpfCnpj,
        StateRegistration,
        MunicipalRegistration,
        PersonType,
        RegistrationDate,
        Status,
        RepresentativeName,
        LandlinePhone,
        MobilePhone,
        PrimaryEmail,
        SecondaryEmail,
        Website,
        Address,
        BankInformation,
        Category,
        PaymentTerms,
        DeliveryTime,
        ShippingMethod,
        OfferedProductsServices,
        SupportingDocuments,
        AdditionalNotes,
        RelationshipHistory);
}

