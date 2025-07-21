using BuildingBlock.Domain.ValueObjects.Contact;
using BuildingBlock.Domain.ValueObjects.Location;

namespace Administration.Domain.Restaurants.Models
{
    public class Restaurant : Entity, IAggregateRoot
    {

        public Restaurant(
            Guid id,
            string name,
            PersonName primaryContact,
            Phone administrativePhone,
            Phone commercialPhone,
            Phone phone,
            Email administrativeEmail,
            Email commercialEmail,
            Email email,
            Address address)
            : base(id)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            PrimaryContact = primaryContact;
            AdministrativePhone = administrativePhone;
            CommercialPhone = commercialPhone;
            Phone = phone;
            AdministrativeEmail = administrativeEmail;
            CommercialEmail = commercialEmail;
            Email = email;
            Address = address;
        }

        public string Name { get; private set; }


        public PersonName PrimaryContact { get; private set; }

        public Phone AdministrativePhone { get; private set; }

        public Phone CommercialPhone { get; private set; }

        public Phone Phone { get; private set; }

        public Email AdministrativeEmail { get; private set; }

        public Email CommercialEmail { get; private set; }

        public Email Email { get; private set; }

        public Address Address { get; private set; }


        //TODO: Cuisine Type: The style of cuisine offered by the restaurant, such as Italian, Mexican, Chinese, etc.

        //TODO: Opening Hours: The hours when the restaurant opens and closes on each day of the week.

        //TODO: Payment Information: Accepted payment methods at the restaurant, such as cash, credit card, debit card, or mobile payment.


        public static Restaurant Create(string name, PersonName primaryContact, Phone administrativePhone, Phone commercialPhone, Phone phone, Email administrativeEmail, Email commercialEmail, Email email, Address address)
        {
            return new Restaurant(
                Guid.NewGuid(),
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
        }
    }
}
