using Administration.Domain.Suppliers;
using Administration.Domain.Suppliers.Entities;
using BuildingBlock.Domain.ValueObjects.Business;
using BuildingBlock.Domain.ValueObjects.Contact;
using BuildingBlock.Domain.ValueObjects.Location;
using FluentAssertions;

namespace Administration.Domain.UnitTest.Suppliers;

public class SupplierTests
{
    [Fact]
    public void Create_ShouldSetPropertiesCorrectly()
    {
        var legalName = LegalName.Parse("Fornecedor Ltda").Value;
        var tradeName = TradeName.Parse("Fornecedor").Value;
        var document = CpfCnpj.Parse("12345678901").Value;
        var stateReg = StateRegistration.Parse("123").Value;
        var municipalReg = MunicipalRegistration.Parse("456").Value;
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
        var bankInfo = BankInformation.Create("Banco", "0001", "12345-6", AccountType.Checking, new[] {"pix@teste.com"}).Value!;
        var category = "Serviços";
        var paymentTerms = "30 dias";
        var deliveryTime = "7 dias";
        var shippingMethod = "Correios";
        var offered = "Consultoria";
        var docs = SupportingDocuments.Parse(null, null, null, null).Value;
        var notes = "Observações";
        var history = "Histórico";

        var dto = new SupplierDto(
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
        var supplier = Supplier.Create(dto);

        supplier.Id.Should().NotBe(Guid.Empty);
        supplier.LegalName.Should().Be(legalName);
        supplier.TradeName.Should().Be(tradeName);
        supplier.CpfCnpj.Should().Be(document);
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
        supplier.SupportingDocuments.Should().Be(docs);
        supplier.AdditionalNotes.Should().Be(notes);
        supplier.RelationshipHistory.Should().Be(history);
    }

    [Fact]
    public void Update_ShouldChangeProperties()
    {
        var supplier = Supplier.Create(new SupplierDto(
            LegalName.Parse("Fornecedor Ltda").Value,
            TradeName.Parse("Fornecedor").Value,
            CpfCnpj.Parse("12345678901").Value,
            StateRegistration.Parse("123").Value,
            MunicipalRegistration.Parse("456").Value,
            PersonType.Legal,
            DateTime.UtcNow,
            SupplierStatus.Active,
            PersonName.Parse("Joao da Silva").Value!,
            Phone.Parse("11999990000").Value!,
            Phone.Parse("11988880000").Value!,
            Email.Parse("contato@teste.com").Value!,
            Email.Parse("contato2@teste.com").Value!,
            "https://teste.com",
            Address.Parse("Rua A", "10", null, "Cidade", "SP", "01000-000").Value!,
            BankInformation.Create("Banco", "0001", "12345-6", AccountType.Checking, new[] {"pix@teste.com"}).Value!,
            "Serviços",
            "30 dias",
            "7 dias",
            "Correios",
            "Consultoria",
            null,
            null,
            null));

        supplier.Update(new SupplierDto(
            LegalName.Parse("Novo Nome").Value,
            TradeName.Parse("Novo Fantasia").Value,
            CpfCnpj.Parse("98765432100").Value,
            StateRegistration.Parse("321").Value,
            MunicipalRegistration.Parse("654").Value,
            PersonType.Physical,
            supplier.RegistrationDate,
            SupplierStatus.Inactive,
            PersonName.Parse("Maria").Value!,
            Phone.Parse("1122223333").Value!,
            Phone.Parse("11999998888").Value!,
            Email.Parse("novo@teste.com").Value!,
            Email.Parse("novo2@teste.com").Value!,
            "https://novo.com",
            Address.Parse("Rua B", "20", null, "Outra", "RJ", "02000-000").Value!,
            BankInformation.Create("Banco2", "0002", "54321-0", AccountType.Savings, new[]{"key"}).Value!,
            "Produtos",
            "60 dias",
            "10 dias",
            "Transportadora",
            "Serviços",
            null,
            "Notas",
            "Historico"));

        supplier.LegalName.Should().Be(LegalName.Parse("Novo Nome").Value);
        supplier.TradeName.Should().Be(TradeName.Parse("Novo Fantasia").Value);
        supplier.CpfCnpj.Should().Be(CpfCnpj.Parse("98765432100").Value);
        supplier.Status.Should().Be(SupplierStatus.Inactive);
    }
}
