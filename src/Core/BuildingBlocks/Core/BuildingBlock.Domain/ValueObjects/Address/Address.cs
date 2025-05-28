using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BuildingBlock.Domain.ValueObjects.Address
{
    public record Address : IValueObject<string>
    {
        public static Address Empty => new Address("Rua da silva", "01", "Não tem", "Cidade", "Estado", "84589-000");

        [Column("Street")]
        public string Street { get; private set; }

        [Column("Number")]
        public string Number { get; private set; }

        [Column("Complement")]
        public string? Complement { get; private set; }

        [Column("City")]
        public string City { get; private set; }

        [Column("State")]
        public string State { get; private set; }

        [Column("ZIPCode")]
        public string ZIPCode { get; private set; }

        private Address(string street, string number, string? complement, string city, string state, string zipCode)
        {
            Street = street;
            Number = number;
            Complement = complement;
            City = city;
            State = state;
            ZIPCode = zipCode;
        }

        public ValidationResult<string> Validate()
        {
            return AddressValidator.IsValid(this.Street, this.Number, this.Complement, this.City, this.State, this.ZIPCode);
        }

        public override string? ToString()
        {
            var @string = new StringBuilder();
            @string.AppendLine($"Street: {Street}, ");
            @string.AppendLine($"Number: {Number}, ");
            @string.AppendLine($"Complement: {Complement}, ");
            @string.AppendLine($"City: {City}, ");
            @string.AppendLine($"State: {State}, ");
            @string.AppendLine($"ZIPCode: {ZIPCode}");

            return @string.ToString();
        }

        public static Address Parse(string? street, string? number, string? complement, string? city, string? state, string? zipCode)
        {
            var result = AddressValidator.IsValid(street, number, complement, city, state, zipCode);
            result.ThrowIfInvalid();

            return new Address(street!, number!, complement!, city!, state!, zipCode!);
        }
    }
}
