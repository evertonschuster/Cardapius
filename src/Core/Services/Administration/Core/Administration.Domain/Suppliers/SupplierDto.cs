using BuildingBlock.Domain.ValueObjects.Business;
using BuildingBlock.Domain.ValueObjects.Contact;
using BuildingBlock.Domain.ValueObjects.Location;

namespace Administration.Domain.Suppliers;

public record SupplierDto(
    LegalName LegalName,
    TradeName TradeName,
    CpfCnpj CpfCnpj,
    StateRegistration StateRegistration,
    MunicipalRegistration MunicipalRegistration,
    PersonType PersonType,
    DateTime RegistrationDate,
    SupplierStatus Status,
    PersonName RepresentativeName,
    Phone LandlinePhone,
    Phone MobilePhone,
    Email PrimaryEmail,
    Email SecondaryEmail,
    string? Website,
    Address Address,
    BankInformation BankInformation,
    string Category,
    string PaymentTerms,
    string DeliveryTime,
    string ShippingMethod,
    string OfferedProductsServices,
    SupportingDocuments? SupportingDocuments,
    string? AdditionalNotes,
    string? RelationshipHistory);
