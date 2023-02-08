using BuildingBlock.Domain.Entities;
using BuildingBlock.Domain.ValueObjects.Emails;

namespace Administration.Domain.Clients
{
    public class Client : Entity, IAggregateRoot
    {
        public Client(Guid id, Email email, string? name) : base(id)
        {
            this.Email = email;
            this.Name = name;
        }


        //public Name Name { get; private set; }
        //public CNPJ CNPJ { get; private set; }

        public Email Email { get; private set; }
        public string? Name { get; private set; }

        //public Telephone CommercialPhone { get; private set; }
        //public Telephone AdministrativePhone { get; private set; }
        //public Adress Adress { get; private set; }

        public static Client Create(Email email, string? name)
        {
            return new Client(Guid.NewGuid(), email, name);
        }
    }
}
