﻿namespace BuildingBlock.Domain.ValueObjects.Address
{
    public readonly struct Address : IValueObject
    {
        public string Street { get; }
        public string Number { get; }
        public string Complement { get; }
        public string City { get; }
        public string State { get; }
        public string ZIPCode { get; }

        public Address(string street, string number, string complement, string city, string state, string zipCode)
        {
            Street = street;
            Number = number;
            Complement = complement;
            City = city;
            State = state;
            ZIPCode = zipCode;
        }

        public Address()
        {
        }

        public bool IsValid()
        {
            return AddressValidator.IsValid(this.Street, this.Number, this.Complement, this.City, this.State, this.ZIPCode);
        }
    }
}