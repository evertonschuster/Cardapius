using Administration.Domain.Suppliers;
using Administration.Domain.Suppliers.Entities;
using BuildingBlock.Domain.ValueObjects.Business;

namespace Administration.Application.Suppliers.Commands.CreateSupplier;

public class CreateSupplierCommand : ICommandRequest<CreateSupplierResult>
{
    // Informações Gerais
    public LegalName LegalName { get; set; }
    public TradeName TradeName { get; set; }
    public CpfCnpj CpfCnpj { get; set; }
    public StateRegistration StateRegistration { get; set; }
    public MunicipalRegistration MunicipalRegistration { get; set; }
    public PersonType PersonType { get; set; }
    public DateTime RegistrationDate { get; set; }
    public SupplierStatus Status { get; set; }

    // Contato
    public PersonName RepresentativeName { get; set; }
    public Phone LandlinePhone { get; set; }
    public Phone MobilePhone { get; set; }
    public Email PrimaryEmail { get; set; }
    public Email SecondaryEmail { get; set; }
    public string? Website { get; set; }

    // Endereço
    public required Address Address { get; set; }

    // Informações Bancárias
    public required BankInformation BankInformation { get; set; }

    // Informações Comerciais
    public string Category { get; set; } = string.Empty;
    public string PaymentTerms { get; set; } = string.Empty;
    public string DeliveryTime { get; set; } = string.Empty;
    public string ShippingMethod { get; set; } = string.Empty;
    public string OfferedProductsServices { get; set; } = string.Empty;

    // Documentações
    public SupportingDocuments? SupportingDocuments { get; set; }

    // Observações
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

    internal Supplier ToModel() => Supplier.Create(ToDto());
}
