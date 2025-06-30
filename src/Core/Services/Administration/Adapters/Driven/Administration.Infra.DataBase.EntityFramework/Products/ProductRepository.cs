using Administration.Application.Products;
using Administration.Domain.Products.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
