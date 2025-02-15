using BuildingBlock.Domain.Exceptions;

namespace BuildingBlock.Domain.ValueObjects.Emails.Exceptions
{
    public class InvalidEmailException : BusinessException
    {
        public InvalidEmailException() : base("Um endereço de e-mail válido deve ser informado.")
        {
        }
    }
}
