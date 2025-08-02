using Administration.Domain.Products.Entities;

namespace Administration.Domain.Products.Repositories
{
    public interface IProductRepository
    {
        Product Create(Product model);
        bool ExistsById(Guid id, CancellationToken cancellation);
        Product? GetWithAllPropertyByIds(Guid productId);
        List<Product> ListWithAllPropertyByIds(List<Guid> productIds);
    }
}
