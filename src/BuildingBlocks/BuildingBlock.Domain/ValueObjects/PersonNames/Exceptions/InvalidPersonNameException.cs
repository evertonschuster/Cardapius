using BuildingBlock.Domain.Exceptions;

namespace BuildingBlock.Domain.ValueObjects.PersonNames.Exceptions
{
    public class InvalidPersonNameException : BusinessException
    {
        public InvalidPersonNameException() : base("Nome inválido!")
        {
        }
    }
}
