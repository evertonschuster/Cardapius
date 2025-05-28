using BuildingBlock.Domain.Exceptions;

namespace BuildingBlock.Domain.ValueObjects.ProductNames.Exceptions
{
    /// <summary>
    /// Lançada quando o nome do produto ou serviço não atinge o comprimento mínimo.
    /// </summary>
    public sealed class ProductNameTooShortException : BusinessException
    {
        public ProductNameTooShortException(int minLength)
            : base($"O nome deve ter pelo menos {minLength} caracteres.") { }
    }
}
