using Administration.Domain.Suppliers.Entities;
using Administration.Domain.Suppliers.Repositories;

namespace Administration.Infra.DataBase.EntityFramework.Suppliers.Repositories;

public class SupplierRepository : Repository<Supplier>, ISupplierRepository
{
    public SupplierRepository(IDbContext context) : base(context)
    {
    }
}
