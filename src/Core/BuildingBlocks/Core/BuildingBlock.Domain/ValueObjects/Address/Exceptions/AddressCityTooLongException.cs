using BuildingBlock.Domain.Exceptions;

namespace BuildingBlock.Domain.ValueObjects.Address.Exceptions
{
    public sealed class AddressCityTooLongException : BusinessException
    {
        public AddressCityTooLongException(int maxCityLength) : base($"O nome da cidade não deve exceder {maxCityLength} caracteres.")
        {
        }
    }
}