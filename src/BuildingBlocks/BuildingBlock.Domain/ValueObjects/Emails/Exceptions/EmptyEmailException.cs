using BuildingBlock.Domain.Exceptions;

namespace BuildingBlock.Domain.ValueObjects.Emails.Exceptions
{
    public class EmptyEmailException : BusinessException
    {
        public EmptyEmailException() : base("Um endereço de e-mail não é válido.")
        {
        }
    }
}
