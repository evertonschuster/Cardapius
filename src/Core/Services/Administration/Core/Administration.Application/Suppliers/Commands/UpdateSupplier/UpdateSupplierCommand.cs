using Administration.Domain.Suppliers;
using Administration.Domain.Suppliers.Entities;
using Administration.Domain.Suppliers.ValueObjects;
using BuildingBlock.Domain.ValueObjects.Contact;
using BuildingBlock.Domain.ValueObjects.Location;
using MediatR;
using System;

namespace Administration.Application.Suppliers.Commands.UpdateSupplier;

public class UpdateSupplierCommand : IRequest<UpdateSupplierResult>
{
    public Guid Id { get; set; }

    public string LegalName { get; set; } = string.Empty;
    public string TradeName { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public string StateRegistration { get; set; } = string.Empty;
    public string MunicipalRegistration { get; set; } = string.Empty;
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

    public Documentations? Documentations { get; set; }
    public string? AdditionalNotes { get; set; }
    public string? RelationshipHistory { get; set; }
}

