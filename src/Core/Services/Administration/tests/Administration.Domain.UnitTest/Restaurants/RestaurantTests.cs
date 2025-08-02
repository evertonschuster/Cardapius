using Administration.Domain.Restaurants.Models;
using BuildingBlock.Domain.ValueObjects.Contact;
using BuildingBlock.Domain.ValueObjects.Location;
using FluentAssertions;

namespace Administration.Domain.UnitTest.Restaurants
{
    public class RestaurantTests
    {
        [Fact]
        public void Constructor_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Restaurante Teste";
            var primaryContact = PersonName.Parse("João da Silva").Value!;
            var administrativePhone = Phone.Parse("11999990000").Value!;
            var commercialPhone = Phone.Parse("11988880000").Value!;
            var phone = Phone.Parse("11977770000").Value!;
            var administrativeEmail = Email.Parse("admin@teste.com").Value!;
            var commercialEmail = Email.Parse("comercial@teste.com").Value!;
            var email = Email.Parse("contato@teste.com").Value!;
            var address = Address.Parse("Rua Teste", "123", "Apto 1", "São Paulo", "SP", "01000-000").Value!;

            // Act
            var restaurant = new Restaurant(
                id,
                name,
                primaryContact,
                administrativePhone,
                commercialPhone,
                phone,
                administrativeEmail,
                commercialEmail,
                email,
                address
            );

            // Assert
            restaurant.Id.Should().Be(id);
            restaurant.Name.Should().Be(name);
            restaurant.PrimaryContact.Should().Be(primaryContact);
            restaurant.AdministrativePhone.Should().Be(administrativePhone);
            restaurant.CommercialPhone.Should().Be(commercialPhone);
            restaurant.Phone.Should().Be(phone);
            restaurant.AdministrativeEmail.Should().Be(administrativeEmail);
            restaurant.CommercialEmail.Should().Be(commercialEmail);
            restaurant.Email.Should().Be(email);
            restaurant.Address.Should().Be(address);
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_WhenNameIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            string? name = null;
            var primaryContact = PersonName.Parse("João da Silva").Value!;
            var administrativePhone = Phone.Parse("11999990000").Value!;
            var commercialPhone = Phone.Parse("11988880000").Value!;
            var phone = Phone.Parse("11977770000").Value!;
            var administrativeEmail = Email.Parse("admin@teste.com").Value!;
            var commercialEmail = Email.Parse("comercial@teste.com").Value!;
            var email = Email.Parse("contato@teste.com").Value!;
            var address = Address.Parse("Rua Teste", "123", "Apto 1", "São Paulo", "SP", "01000-000").Value!;

            // Act
            Action act = () => new Restaurant(
                id,
                name!,
                primaryContact,
                administrativePhone,
                commercialPhone,
                phone,
                administrativeEmail,
                commercialEmail,
                email,
                address
            );

            // Assert
            act.Should().Throw<ArgumentNullException>().WithParameterName("name");
        }

        [Fact]
        public void Create_ShouldReturnRestaurantWithGeneratedId()
        {
            // Arrange
            var name = "Restaurante Teste";
            var primaryContact = PersonName.Parse("João da Silva").Value!;
            var administrativePhone = Phone.Parse("11999990000").Value!;
            var commercialPhone = Phone.Parse("11988880000").Value!;
            var phone = Phone.Parse("11977770000").Value!;
            var administrativeEmail = Email.Parse("admin@teste.com").Value!;
            var commercialEmail = Email.Parse("comercial@teste.com").Value!;
            var email = Email.Parse("contato@teste.com").Value!;
            var address = Address.Parse("Rua Teste", "123", "Apto 1", "São Paulo", "SP", "01000-000").Value!;

            // Act
            var restaurant = Restaurant.Create(
                name,
                primaryContact,
                administrativePhone,
                commercialPhone,
                phone,
                administrativeEmail,
                commercialEmail,
                email,
                address
            );

            // Assert
            restaurant.Id.Should().NotBe(Guid.Empty);
            restaurant.Name.Should().Be(name);
            restaurant.PrimaryContact.Should().Be(primaryContact);
            restaurant.AdministrativePhone.Should().Be(administrativePhone);
            restaurant.CommercialPhone.Should().Be(commercialPhone);
            restaurant.Phone.Should().Be(phone);
            restaurant.AdministrativeEmail.Should().Be(administrativeEmail);
            restaurant.CommercialEmail.Should().Be(commercialEmail);
            restaurant.Email.Should().Be(email);
            restaurant.Address.Should().Be(address);
        }
    }
}
