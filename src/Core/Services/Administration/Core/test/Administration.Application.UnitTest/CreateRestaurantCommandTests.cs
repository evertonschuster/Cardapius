using Administration.Application.Restaurants.Commands.CreateRestaurant;
using BuildingBlock.Domain.ValueObjects.PersonNames;
using BuildingBlock.Domain.ValueObjects.Phones;
using BuildingBlock.Domain.ValueObjects.Emails;
using BuildingBlock.Domain.ValueObjects.Address;

public class CreateRestaurantCommandTests
{
    [Fact]
    public void ToModel_Should_Map_Phones_In_Correct_Order()
    {
        // Arrange
        var command = new CreateRestaurantCommand
        {
            Name = "My Restaurant",
            PrimaryContact = PersonName.Parse("John Doe").Value,
            AdministrativePhone = Phone.Parse("111").Value,
            CommercialPhone = Phone.Parse("222").Value,
            Phone = Phone.Parse("333").Value,
            AdministrativeEmail = Email.Parse("admin@example.com").Value,
            CommercialEmail = Email.Parse("commercial@example.com").Value,
            Email = Email.Parse("info@example.com").Value,
            Address = Address.Empty
        };

        // Act
        var restaurant = command.ToModel();

        // Assert
        restaurant.AdministrativePhone.Should().Be(command.AdministrativePhone);
        restaurant.CommercialPhone.Should().Be(command.CommercialPhone);
        restaurant.Phone.Should().Be(command.Phone);
    }
}
