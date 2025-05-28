using BuildingBlock.Domain.Exceptions;

namespace BuildingBlock.Domain.ValueObjects.Address.Exceptions
{
    public sealed class AddressStateEmptyException : BusinessException
    {
        public AddressStateEmptyException() : base("O estado não pode estar vazio.")
        {
        }
    }
}