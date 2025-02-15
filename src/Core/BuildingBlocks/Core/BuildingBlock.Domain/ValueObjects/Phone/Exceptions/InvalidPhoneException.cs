using BuildingBlock.Domain.Exceptions;
namespace BuildingBlock.Domain.ValueObjects.Phones.Exceptions
{
    public class InvalidPhoneException : BusinessException
    {
        public InvalidPhoneException() : base("Telefone inválido.")
        {
        }
    }
}
