using BuildingBlock.Domain.Exceptions;

namespace BuildingBlock.Domain.ValueObjects.Address.Exceptions
{
    public sealed class AddressComplementTooLongException : BusinessException
    {
        public AddressComplementTooLongException(int maxComplementLength)
            : base($"O complemento não deve exceder {maxComplementLength} caracteres.")
        {
        }
    }
}