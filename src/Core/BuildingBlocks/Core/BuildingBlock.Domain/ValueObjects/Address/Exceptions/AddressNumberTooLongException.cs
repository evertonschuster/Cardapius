using BuildingBlock.Domain.Exceptions;

namespace BuildingBlock.Domain.ValueObjects.Address.Exceptions
{
    public sealed class AddressNumberTooLongException : BusinessException
    {
        public AddressNumberTooLongException(int maxNumberLength) : base($"O número do endereço não deve exceder {maxNumberLength} caracteres.")
        {
        }
    }
}