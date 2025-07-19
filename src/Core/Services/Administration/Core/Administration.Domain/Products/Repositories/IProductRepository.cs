using Administration.Application.Products;

namespace Administration.Domain.Products.Repositories
{
    public interface IProductRepository
    {
        Product Create(Product model);
        bool ExistsById(Guid id, CancellationToken cancellation);
        List<Product> GetWithAllPropertyByIds(List<Guid> sideDishes);
    }
}
