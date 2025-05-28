using BuildingBlock.Domain.Exceptions;

namespace BuildingBlock.Domain.ValueObjects.Address.Exceptions
{
    public sealed class AddressZIPCodeEmptyException : BusinessException
    {
        public AddressZIPCodeEmptyException() : base("O CEP não pode estar vazio.")
        {
        }
    }
}