using FluentValidation;

namespace Administration.Application.Clients.Commands
{
    public class CreateClientValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientValidator()
        {

        }
    }
}
