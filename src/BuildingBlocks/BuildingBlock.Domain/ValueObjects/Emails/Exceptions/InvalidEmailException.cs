using BuildingBlock.Domain.Exceptions;

namespace BuildingBlock.Domain.ValueObject.Email.Exceptions
{
    public class InvalidEmailException : BusinessException
    {
        public InvalidEmailException() : base("Um endereço de e-mail válido deve ser informado.")
        {
        }
    }
}
