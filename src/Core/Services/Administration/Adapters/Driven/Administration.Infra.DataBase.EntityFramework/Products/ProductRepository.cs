using Administration.Domain.Products.Entities;
using Administration.Domain.Products.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Administration.Infra.DataBase.EntityFramework.Products
{
    internal class ProductRepository(AdministrationDbContext dbContext) : IProductRepository
    {
        /// <summary>
        /// Adds a new product and its associated side dishes to the database context.
        /// </summary>
        /// <param name="model">The product entity to add, including its related side dishes.</param>
        /// <returns>The product entity that was added to the context.</returns>
        public Product Create(Product model)
        {
            dbContext.AttachRange(model.SideDishes);
            dbContext.Add(model);

            return model;
        }

        /// <summary>
        /// Determines whether a product with the specified ID exists in the database.
        /// </summary>
        /// <param name="id">The unique identifier of the product to check for existence.</param>
        /// <param name="cancellation">A cancellation token (not used in this method).</param>
        /// <returns>True if a product with the given ID exists; otherwise, false.</returns>
        public bool ExistsById(Guid id, CancellationToken cancellation)
        {
            return dbContext.Products
                .Any(x => x.Id == id);

        }

        /// <summary>
        /// Retrieves a product by its ID, including its associated side dishes.
        /// </summary>
        /// <param name="productId">The unique identifier of the product to retrieve.</param>
        /// <returns>The product with its related side dishes if found; otherwise, null.</returns>
        public Product? GetWithAllPropertyByIds(Guid productId)
        {
            return dbContext.Products
                .Where(x => x.Id == productId)
                .Include(x => x.SideDishes)
                .AsSplitQuery()
                .FirstOrDefault();
        }

        /// <summary>
        /// Retrieves a list of products matching the specified IDs, including their associated side dishes.
        /// </summary>
        /// <param name="productIds">A list of product IDs to retrieve.</param>
        /// <returns>A list of products with their related side dishes included. Returns an empty list if no products are found.</returns>
        public List<Product> ListWithAllPropertyByIds(List<Guid> productIds)
        {
            return dbContext.Products
                .Where(x => productIds.Contains(x.Id))
                .Include(x => x.SideDishes)
                .AsSplitQuery()
                .ToList();
        }
    }
}
