using Administration.Domain.Suppliers.Entities;
using Administration.Domain.Suppliers.ValueObjects;
using Administration.Domain.Suppliers;
using BuildingBlock.Domain.ValueObjects.Contact;
using BuildingBlock.Domain.ValueObjects.Location;
using FluentAssertions;
using Xunit;

namespace Administration.Domain.UnitTest.Suppliers;

public class SupplierTests
{
    [Fact]
    public void Create_ShouldSetPropertiesCorrectly()
    {
        var legalName = "Fornecedor Ltda";
        var tradeName = "Fornecedor";
        var document = "123456789";
        var stateReg = "123";
        var municipalReg = "456";
        var personType = PersonType.Legal;
        var registrationDate = DateTime.UtcNow;
        var status = SupplierStatus.Active;
        var representative = PersonName.Parse("Joao da Silva").Value!;
        var landline = Phone.Parse("11999990000").Value!;
        var mobile = Phone.Parse("11988880000").Value!;
        var primaryEmail = Email.Parse("contato@teste.com").Value!;
        var secondaryEmail = Email.Parse("contato2@teste.com").Value!;
        var website = "https://teste.com";
        var address = Address.Parse("Rua A", "10", null, "Cidade", "SP", "01000-000").Value!;
        var bankInfo = new BankInformation("Banco", "0001", "12345-6", AccountType.Checking, "pix@teste.com");
        var category = "Serviços";
        var paymentTerms = "30 dias";
        var deliveryTime = "7 dias";
        var shippingMethod = "Correios";
        var offered = "Consultoria";
        var docs = new Documentations(null, null, null, null);
        var notes = "Observações";
        var history = "Histórico";

        var supplier = Supplier.Create(
            legalName,
            tradeName,
            document,
            stateReg,
            municipalReg,
            personType,
            registrationDate,
            status,
            representative,
            landline,
            mobile,
            primaryEmail,
            secondaryEmail,
            website,
            address,
            bankInfo,
            category,
            paymentTerms,
            deliveryTime,
            shippingMethod,
            offered,
            docs,
            notes,
            history);

        supplier.Id.Should().NotBe(Guid.Empty);
        supplier.LegalName.Should().Be(legalName);
        supplier.TradeName.Should().Be(tradeName);
        supplier.Document.Should().Be(document);
        supplier.StateRegistration.Should().Be(stateReg);
        supplier.MunicipalRegistration.Should().Be(municipalReg);
        supplier.PersonType.Should().Be(personType);
        supplier.Status.Should().Be(status);
        supplier.RepresentativeName.Should().Be(representative);
        supplier.LandlinePhone.Should().Be(landline);
        supplier.MobilePhone.Should().Be(mobile);
        supplier.PrimaryEmail.Should().Be(primaryEmail);
        supplier.SecondaryEmail.Should().Be(secondaryEmail);
        supplier.Website.Should().Be(website);
        supplier.Address.Should().Be(address);
        supplier.BankInformation.Should().Be(bankInfo);
        supplier.Category.Should().Be(category);
        supplier.PaymentTerms.Should().Be(paymentTerms);
        supplier.DeliveryTime.Should().Be(deliveryTime);
        supplier.ShippingMethod.Should().Be(shippingMethod);
        supplier.OfferedProductsServices.Should().Be(offered);
        supplier.Documentations.Should().Be(docs);
        supplier.AdditionalNotes.Should().Be(notes);
        supplier.RelationshipHistory.Should().Be(history);
    }
}
