using System.ComponentModel.DataAnnotations.Schema;

namespace Administration.Domain.Restaurants.Models
{
    public class Restaurant : Entity, IAggregateRoot
    {
        protected Restaurant()
        {
        }

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


        [NotMapped]
        public PersonName PrimaryContact { get; private set; }

        [NotMapped]
        public Phone AdministrativePhone { get; private set; }
        [NotMapped]
        public Phone CommercialPhone { get; private set; }
        [NotMapped]
        public Phone Phone { get; private set; }

        [NotMapped]
        public Email AdministrativeEmail { get; private set; }
        [NotMapped]
        public Email CommercialEmail { get; private set; }
        public Email Email { get; private set; }

        [NotMapped]
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
