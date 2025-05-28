using BuildingBlock.Domain.Exceptions;

namespace BuildingBlock.Domain.ValueObjects.Address.Exceptions
{
    public sealed class AddressCityEmptyException : BusinessException
    {
        public AddressCityEmptyException() : base("A cidade não pode estar vazia.")
        {
        }
    }
}