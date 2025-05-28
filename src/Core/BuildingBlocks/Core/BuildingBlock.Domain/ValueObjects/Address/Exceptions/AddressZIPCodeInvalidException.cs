using BuildingBlock.Domain.Exceptions;

namespace BuildingBlock.Domain.ValueObjects.Address.Exceptions
{
    public sealed class AddressZIPCodeInvalidException : BusinessException
    {
        public AddressZIPCodeInvalidException()
            : base("O CEP é inválido. Ele deve estar no formato XXXXX-XXX ou XXXXXXXX.")
        {
        }
    }
}