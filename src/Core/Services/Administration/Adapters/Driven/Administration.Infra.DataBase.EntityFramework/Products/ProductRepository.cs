using Administration.Domain.Products.Entities;
using Administration.Domain.Products.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Administration.Infra.DataBase.EntityFramework.Products
{
    internal class ProductRepository(AdministrationDbContext dbContext) : IProductRepository
    {
        public Product Create(Product model)
        {
            dbContext.AttachRange(model.SideDishes);
            dbContext.Add(model);

            return model;
        }

        public bool ExistsById(Guid id, CancellationToken cancellation)
        {
            return dbContext.Products
                .Any(x => x.Id == id);

        }

        public Product? GetWithAllPropertyByIds(Guid productId)
        {
            return dbContext.Products
                .Where(x => x.Id == productId)
                .Include(x => x.SideDishes)
                .AsSplitQuery()
                .FirstOrDefault();
        }

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
