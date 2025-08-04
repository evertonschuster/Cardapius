using Administration.Domain.Suppliers.DomainEvents;
using Administration.Domain.Suppliers;
using BuildingBlock.Domain.ValueObjects.Business;
using BuildingBlock.Domain.ValueObjects.Contact;
using BuildingBlock.Domain.ValueObjects.Location;

namespace Administration.Domain.Suppliers.Entities;

public class Supplier : Entity
{
    protected Supplier()
    {
    }

    public Supplier(
        Guid id,
        LegalName legalName,
        TradeName tradeName,
        CpfCnpj cpfCnpj,
        StateRegistration stateRegistration,
        MunicipalRegistration municipalRegistration,
        PersonType personType,
        DateTime registrationDate,
        SupplierStatus status,
        PersonName representativeName,
        Phone landlinePhone,
        Phone mobilePhone,
        Email primaryEmail,
        Email secondaryEmail,
        string? website,
        Address address,
        BankInformation bankInformation,
        string category,
        string paymentTerms,
        string deliveryTime,
        string shippingMethod,
        string offeredProductsServices,
        SupportingDocuments? supportingDocuments,
        string? additionalNotes,
        string? relationshipHistory)
        : base(id)
    {
        LegalName = legalName;
        TradeName = tradeName;
        CpfCnpj = cpfCnpj;
        StateRegistration = stateRegistration;
        MunicipalRegistration = municipalRegistration;
        PersonType = personType;
        RegistrationDate = registrationDate;
        Status = status;
        RepresentativeName = representativeName;
        LandlinePhone = landlinePhone;
        MobilePhone = mobilePhone;
        PrimaryEmail = primaryEmail;
        SecondaryEmail = secondaryEmail;
        Website = website;
        Address = address;
        BankInformation = bankInformation;
        Category = category;
        PaymentTerms = paymentTerms;
        DeliveryTime = deliveryTime;
        ShippingMethod = shippingMethod;
        OfferedProductsServices = offeredProductsServices;
        SupportingDocuments = supportingDocuments;
        AdditionalNotes = additionalNotes;
        RelationshipHistory = relationshipHistory;
    }

    public LegalName LegalName { get; private set; }
    public TradeName TradeName { get; private set; }
    public CpfCnpj CpfCnpj { get; private set; }
    public StateRegistration StateRegistration { get; private set; }
    public MunicipalRegistration MunicipalRegistration { get; private set; }
    public PersonType PersonType { get; private set; }
    public DateTime RegistrationDate { get; private set; }
    public SupplierStatus Status { get; private set; }

    public PersonName RepresentativeName { get; private set; }
    public Phone LandlinePhone { get; private set; }
    public Phone MobilePhone { get; private set; }
    public Email PrimaryEmail { get; private set; }
    public Email SecondaryEmail { get; private set; }
    public string? Website { get; private set; }
    public Address Address { get; private set; }
    public BankInformation BankInformation { get; private set; }

    public string Category { get; private set; }
    public string PaymentTerms { get; private set; }
    public string DeliveryTime { get; private set; }
    public string ShippingMethod { get; private set; }
    public string OfferedProductsServices { get; private set; }

    public SupportingDocuments? SupportingDocuments { get; private set; }
    public string? AdditionalNotes { get; private set; }
    public string? RelationshipHistory { get; private set; }

    public static Supplier Create(SupplierDto dto)
    {
        var model = new Supplier(
            Guid.CreateVersion7(),
            dto.LegalName,
            dto.TradeName,
            dto.CpfCnpj,
            dto.StateRegistration,
            dto.MunicipalRegistration,
            dto.PersonType,
            dto.RegistrationDate,
            dto.Status,
            dto.RepresentativeName,
            dto.LandlinePhone,
            dto.MobilePhone,
            dto.PrimaryEmail,
            dto.SecondaryEmail,
            dto.Website,
            dto.Address,
            dto.BankInformation,
            dto.Category,
            dto.PaymentTerms,
            dto.DeliveryTime,
            dto.ShippingMethod,
            dto.OfferedProductsServices,
            dto.SupportingDocuments,
            dto.AdditionalNotes,
            dto.RelationshipHistory);

        model.AddDomainEvent(new SupplierCreatedEvent<Supplier>(model.Id, null, model));
        return model;
    }

    public void Update(SupplierDto dto)
    {
        var before = (Supplier)MemberwiseClone();

        LegalName = dto.LegalName;
        TradeName = dto.TradeName;
        CpfCnpj = dto.CpfCnpj;
        StateRegistration = dto.StateRegistration;
        MunicipalRegistration = dto.MunicipalRegistration;
        PersonType = dto.PersonType;
        RegistrationDate = dto.RegistrationDate;
        Status = dto.Status;
        RepresentativeName = dto.RepresentativeName;
        LandlinePhone = dto.LandlinePhone;
        MobilePhone = dto.MobilePhone;
        PrimaryEmail = dto.PrimaryEmail;
        SecondaryEmail = dto.SecondaryEmail;
        Website = dto.Website;
        Address = dto.Address;
        BankInformation = dto.BankInformation;
        Category = dto.Category;
        PaymentTerms = dto.PaymentTerms;
        DeliveryTime = dto.DeliveryTime;
        ShippingMethod = dto.ShippingMethod;
        OfferedProductsServices = dto.OfferedProductsServices;
        SupportingDocuments = dto.SupportingDocuments;
        AdditionalNotes = dto.AdditionalNotes;
        RelationshipHistory = dto.RelationshipHistory;

        AddDomainEvent(new SupplierUpdatedEvent<Supplier>(Id, before, this));
    }
}
