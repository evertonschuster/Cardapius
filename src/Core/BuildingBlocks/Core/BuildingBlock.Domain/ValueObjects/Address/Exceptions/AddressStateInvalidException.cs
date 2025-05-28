using BuildingBlock.Domain.Exceptions;

namespace BuildingBlock.Domain.ValueObjects.Address.Exceptions
{
    public sealed class AddressStateInvalidException : BusinessException
    {
        public AddressStateInvalidException()
            : base("O código do estado deve ter exatamente 2 caracteres.")
        {
        }
    }
}