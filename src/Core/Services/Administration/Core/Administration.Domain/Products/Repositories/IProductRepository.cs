using Administration.Application.Products;

namespace Administration.Domain.Products.Repositories
{
    public interface IProductRepository
    {
        object Create(Product model);
    }
}
