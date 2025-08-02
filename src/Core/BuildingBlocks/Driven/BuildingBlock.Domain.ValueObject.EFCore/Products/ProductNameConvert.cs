using BuildingBlock.Domain.ValueObjects.Products;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BuildingBlock.Infra.Domain.ValueObjects.EFCore.Products
{
    internal class ProductNameConvert : ValueConverter<ProductName, string>
    {
        /// <summary>
        /// Initializes a value converter for mapping between the <c>ProductName</c> value object and its string representation for Entity Framework Core.
        /// </summary>
        public ProductNameConvert() : base(v => v.Value, v => ProductName.Parse(v).Value)
        {
        }
    }
}
