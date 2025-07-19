using Administration.Application.Products;
using Administration.Domain.Products.Repositories;

namespace Administration.Infra.DataBase.EntityFramework.Products
{
    internal class ProductRepository : IProductRepository
    {
        public object Create(Product model)
        {
            throw new NotImplementedException();
        }
    }
}
