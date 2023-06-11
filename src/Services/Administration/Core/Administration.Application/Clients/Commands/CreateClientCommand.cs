using Administration.Domain.Clients;
using BuildingBlock.Domain.ValueObjects.Emails;
using System.ComponentModel.DataAnnotations;

namespace Administration.Application.Clients.Commands
{
    public class CreateClientCommand
    {
        public int Id { get; set; }
        public Email Email { get; set; }
    }
}
