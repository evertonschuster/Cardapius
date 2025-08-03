using Administration.Domain.Suppliers;
using Administration.Domain.Suppliers.DomainEvents;
using BuildingBlock.Domain.Entities;
using BuildingBlock.Domain.ValueObjects.Business;
using BuildingBlock.Domain.ValueObjects.Contact;
using BuildingBlock.Domain.ValueObjects.Location;

namespace Administration.Domain.Suppliers.Entities;

public class Supplier : Entity, IAggregateRoot
{
    protected Supplier() { }

    public Supplier(
        Guid id,
        LegalName legalName,
        TradeName tradeName,
        Document document,
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
        Documentations? documentations,
        string? additionalNotes,
        string? relationshipHistory)
        : base(id)
    {
        LegalName = legalName;
        TradeName = tradeName;
        Document = document;
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
        Documentations = documentations;
        AdditionalNotes = additionalNotes;
        RelationshipHistory = relationshipHistory;
    }

    public LegalName LegalName { get; private set; }
    public TradeName TradeName { get; private set; }
    public Document Document { get; private set; }
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

    public Documentations? Documentations { get; private set; }
    public string? AdditionalNotes { get; private set; }
    public string? RelationshipHistory { get; private set; }

    public static Supplier Create(
        LegalName legalName,
        TradeName tradeName,
        Document document,
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
        Documentations? documentations,
        string? additionalNotes,
        string? relationshipHistory)
    {
        var model = new Supplier(
            Guid.CreateVersion7(),
            legalName,
            tradeName,
            document,
            stateRegistration,
            municipalRegistration,
            personType,
            registrationDate,
            status,
            representativeName,
            landlinePhone,
            mobilePhone,
            primaryEmail,
            secondaryEmail,
            website,
            address,
            bankInformation,
            category,
            paymentTerms,
            deliveryTime,
            shippingMethod,
            offeredProductsServices,
            documentations,
            additionalNotes,
            relationshipHistory);

        model.AddDomainEvent(new SupplierCreatedEvent<Supplier>(model.Id, null, model));
        return model;
    }

    public void Update(
        LegalName legalName,
        TradeName tradeName,
        Document document,
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
        Documentations? documentations,
        string? additionalNotes,
        string? relationshipHistory)
    {
        var before = (Supplier)MemberwiseClone();

        LegalName = legalName;
        TradeName = tradeName;
        Document = document;
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
        Documentations = documentations;
        AdditionalNotes = additionalNotes;
        RelationshipHistory = relationshipHistory;

        AddDomainEvent(new SupplierUpdatedEvent<Supplier>(Id, before, this));
    }
}
