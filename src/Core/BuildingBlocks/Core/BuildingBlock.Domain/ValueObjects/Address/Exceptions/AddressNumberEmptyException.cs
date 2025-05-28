using BuildingBlock.Domain.Exceptions;

namespace BuildingBlock.Domain.ValueObjects.Address.Exceptions
{
    public sealed class AddressNumberEmptyException : BusinessException
    {
        public AddressNumberEmptyException()
            : base("Número do endereço não pode ser vazio.")
        {
        }
    }
}