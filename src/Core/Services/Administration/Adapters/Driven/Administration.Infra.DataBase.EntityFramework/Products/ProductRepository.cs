using Administration.Application.Products;
using Administration.Domain.Products.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Administration.Infra.DataBase.EntityFramework.Products
{
    internal class ProductRepository(AdministrationDbContext dbContext) : IProductRepository
    {
        public Product Create(Product model)
        {
            dbContext.Add(model);

            return model;
        }

        public bool ExistsById(Guid id, CancellationToken cancellation)
        {
            return false;
        }

        public List<Product> GetWithAllPropertyByIds(List<Guid> sideDishes)
        {
            return dbContext.Products
                .Where(x => sideDishes.Contains(x.Id))
                .Include(x => x.SideDishes)
                .ToList();
        }
    }
}
