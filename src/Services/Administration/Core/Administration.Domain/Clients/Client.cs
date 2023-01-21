using BuildingBlock.Domain.Entities;
using BuildingBlock.Domain.ValueObjects.Emails;

namespace Administration.Domain.Clients
{
    public class Client : Entity, IAggregateRoot
    {
        public Client(Guid id, Email email) : base(id)
        {
            this.Email = email;
        }


        //public Name Name { get; private set; }
        //public CNPJ CNPJ { get; private set; }

        public Email Email { get; private set; }


        //public Telephone CommercialPhone { get; private set; }
        //public Telephone AdministrativePhone { get; private set; }
        //public Adress Adress { get; private set; }

        public static Client Create(Email name)
        {
            return new Client(Guid.NewGuid(), name);
        }
    }
}
