using Administration.Domain.Suppliers.Entities;
using Administration.Domain.Suppliers;
using BuildingBlock.Domain.ValueObjects.Business;
using BuildingBlock.Domain.ValueObjects.Contact;
using BuildingBlock.Domain.ValueObjects.Location;

namespace Administration.Application.Suppliers.Commands.CreateSupplier;

public class CreateSupplierCommand : IRequest<CreateSupplierResult>
{
    // Informações Gerais
    public LegalName LegalName { get; set; }
    public TradeName TradeName { get; set; }
    public Document Document { get; set; }
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
    public Documentations? Documentations { get; set; }

    // Observações
    public string? AdditionalNotes { get; set; }
    public string? RelationshipHistory { get; set; }

    internal Supplier ToModel()
    {
        return Supplier.Create(
            LegalName,
            TradeName,
            Document,
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
            Documentations,
            AdditionalNotes,
            RelationshipHistory);
    }
}
