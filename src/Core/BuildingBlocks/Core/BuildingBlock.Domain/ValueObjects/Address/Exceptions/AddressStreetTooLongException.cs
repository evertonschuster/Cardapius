using BuildingBlock.Domain.Exceptions;

namespace BuildingBlock.Domain.ValueObjects.Address.Exceptions
{
    public sealed class AddressStreetTooLongException : BusinessException
    {
        public AddressStreetTooLongException(int maxStreetLength) : base($"O campo 'Rua' deve ter no máximo {maxStreetLength} caracteres.")
        {
        }
    }
}