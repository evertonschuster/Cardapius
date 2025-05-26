using BuildingBlock.Infra.DataBase.MongoDB;
using BuildingBlock.Infra.DataBase.Repositories;
using Store.Domain.Products.Entities;
using Store.Domain.Products.Repositories;

namespace Store.Infra.DataBase.MongoDb.Products.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppMongoClient appMongoClient) : base(appMongoClient)
        {
        }
    }
}
