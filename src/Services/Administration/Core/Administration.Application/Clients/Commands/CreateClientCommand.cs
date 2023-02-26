using Administration.Domain.Clients;
using BuildingBlock.Domain.ValueObjects.Emails;
using System.ComponentModel.DataAnnotations;

namespace Administration.Application.Clients.Commands
{
    public class CreateClientCommand
    {
        public Email Name123 { get; set; }
        public DateTime Namea { get; set; }

        [Required]
        public string? MyProperty { get; set; }

        public Client ToEntity()
        {
            return Client.Create(this.Name123);
        }
    }
}
