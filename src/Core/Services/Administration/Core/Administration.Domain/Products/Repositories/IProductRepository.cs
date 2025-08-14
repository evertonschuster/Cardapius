using Administration.Domain.Products.Entities;

namespace Administration.Domain.Products.Repositories
{
    public interface IProductRepository
    {
        /// <summary>
        /// Creates a new product entity in the repository.
        /// </summary>
        /// <param name="model">The product entity to be created.</param>
        /// <returns>The created product entity.</returns>
        Product Create(Product model);
        /// <summary>
        /// Determines whether a product with the specified unique identifier exists.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <param name="cancellation">A token to monitor for cancellation requests.</param>
        /// <returns><c>true</c> if the product exists; otherwise, <c>false</c>.</returns>
        bool ExistsById(Guid id, CancellationToken cancellation);
        /// <summary>
        /// Retrieves a product by its unique identifier, including all associated properties.
        /// </summary>
        /// <param name="productId">The unique identifier of the product to retrieve.</param>
        /// <returns>The product with all properties if found; otherwise, null.</returns>
        Product? GetWithAllPropertyByIds(Guid productId);
        /// <summary>
        /// Retrieves a list of products by their IDs, including all associated properties.
        /// </summary>
        /// <param name="productIds">A list of product unique identifiers.</param>
        /// <returns>A list of products with all properties populated. Returns an empty list if no products are found.</returns>
        List<Product> ListWithAllPropertyByIds(List<Guid> productIds);
    }
}
