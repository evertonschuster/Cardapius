using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BuildingBlock.Domain.ValueObjects.Address
{
    public record Address : IValueObject
    {
        public static Address Empty => new Address("Rua da silva", "01", "Não tem", "Cidade", "Estado", "84589-000");

        [Column("Street")]
        public string Street { get; init; }

        [Column("Number")]
        public string Number { get; init; }

        [Column("Complement")]
        public string? Complement { get; init; }

        [Column("City")]
        public string City { get; init; }

        [Column("State")]
        public string State { get; init; }

        [Column("ZIPCode")]
        public string ZIPCode { get; init; }

        private Address()
        {
            Street = string.Empty;
            Number = string.Empty;
            Complement = null;
            City = string.Empty;
            State = string.Empty;
            ZIPCode = string.Empty;
        }

        private Address(string street, string number, string? complement, string city, string state, string zipCode)
        {
            Street = street;
            Number = number;
            Complement = complement;
            City = city;
            State = state;
            ZIPCode = zipCode;
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

        public static Result<Address> Parse(string? street, string? number, string? complement, string? city, string? state, string? zipCode)
        {
            var result = AddressValidator.Validate(street, number, complement, city, state, zipCode);
            return Result<Address>.FromValidation(result, () => new Address(street!, number!, complement!, city!, state!, zipCode!));
        }
    }
}
