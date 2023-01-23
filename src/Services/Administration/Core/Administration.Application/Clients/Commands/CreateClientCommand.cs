using Administration.Domain.Clients;
using BuildingBlock.Domain.ValueObjects.Emails;

namespace Administration.Application.Clients.Commands
{
    public class CreateClientCommand
    {
        public Email Name { get; set; }


        public Client ToEntity()
        {
            return Client.Create(this.Name);
        }
    }
}
