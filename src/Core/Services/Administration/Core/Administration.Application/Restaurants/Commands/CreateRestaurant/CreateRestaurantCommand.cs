using Administration.Domain.Restaurants.Models;

namespace Administration.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommand : IRequest<CreateRestaurantResult>
    {
        public string Name { get; set; } = string.Empty;


        public PersonName PrimaryContact { get; set; }


        public Phone AdministrativePhone { get; set; }
        public Phone CommercialPhone { get; set; }
        public Phone Phone { get; set; }


        public Email AdministrativeEmail { get; set; }
        public Email CommercialEmail { get; set; }
        public Email Email { get; set; }

        public Address Address { get; set; }


        //TODO: Cuisine Type: The style of cuisine offered by the restaurant, such as Italian, Mexican, Chinese, etc.

        //TODO: Opening Hours: The hours when the restaurant opens and closes on each day of the week.

        //TODO: Payment Information: Accepted payment methods at the restaurant, such as cash, credit card, debit card, or mobile payment.


        internal Restaurant ToModel()
        {
            return Restaurant.Create(
                this.Name,
                this.PrimaryContact,
                this.AdministrativePhone,
                this.CommercialPhone,
                this.Phone,
                this.AdministrativeEmail,
                this.CommercialEmail,
                this.Email,
                Address.Empty
                );
        }
    }
}
