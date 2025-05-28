using BuildingBlock.Domain.Exceptions;

namespace BuildingBlock.Domain.ValueObjects.Address.Exceptions
{
    public sealed class AddressStreetEmptyException : BusinessException
    {
        public AddressStreetEmptyException() : base("O campo 'Rua' do endereço não pode ser vazio.")
        {
        }
    }
}
