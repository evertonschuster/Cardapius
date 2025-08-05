using BuildingBlock.Domain.ValueObjects.Contact;
using BuildingBlock.Domain.ValueObjects.Location;

namespace Administration.Domain.Restaurants.Models
{
    public class Restaurant : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Restaurant"/> class for ORM or serialization purposes.
        /// </summary>
        protected Restaurant()
        {
            Name = string.Empty;
            Address = Address.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Restaurant"/> class with the specified identifier, name, contact information, and address.
        /// </summary>
        /// <param name="id">The unique identifier for the restaurant.</param>
        /// <param name="name">The name of the restaurant. Cannot be null.</param>
        /// <param name="primaryContact">The primary contact person for the restaurant.</param>
        /// <param name="administrativePhone">The administrative phone number.</param>
        /// <param name="commercialPhone">The commercial phone number.</param>
        /// <param name="phone">The general phone number.</param>
        /// <param name="administrativeEmail">The administrative email address.</param>
        /// <param name="commercialEmail">The commercial email address.</param>
        /// <param name="email">The general email address.</param>
        /// <param name="address">The address of the restaurant.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="name"/> is null.</exception>
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

        /// <summary>
        /// Creates a new <see cref="Restaurant"/> instance with a unique identifier and the specified contact and address information.
        /// </summary>
        /// <param name="name">The name of the restaurant.</param>
        /// <param name="primaryContact">The primary contact person for the restaurant.</param>
        /// <param name="administrativePhone">The administrative phone number.</param>
        /// <param name="commercialPhone">The commercial phone number.</param>
        /// <param name="phone">The general phone number.</param>
        /// <param name="administrativeEmail">The administrative email address.</param>
        /// <param name="commercialEmail">The commercial email address.</param>
        /// <param name="email">The general email address.</param>
        /// <param name="address">The address of the restaurant.</param>
        /// <returns>A new <see cref="Restaurant"/> instance initialized with the provided information.</returns>


        public static Restaurant Create(string name, PersonName primaryContact, Phone administrativePhone, Phone commercialPhone, Phone phone, Email administrativeEmail, Email commercialEmail, Email email, Address address)
        {
            return new Restaurant(
                Guid.CreateVersion7(),
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
