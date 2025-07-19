using BuildingBlock.Domain.ValueObjects.Products;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BuildingBlock.Infra.Domain.ValueObjects.EFCore.Products
{
    internal class ProductNameConvert : ValueConverter<ProductName, string>
    {
        public ProductNameConvert() : base(v => v.Value, v => ProductName.Parse(v).Value)
        {
        }
    }
}
