using Administration.Domain.Suppliers.Entities;
using Administration.Domain.Suppliers.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Administration.Infra.DataBase.EntityFramework.Suppliers.Repositories;

public class SupplierRepository(IDbContext context) : Repository<Supplier>(context), ISupplierRepository
{
    public Task<List<Supplier>> ListAsync(CancellationToken cancellationToken)
    {
        return _IDbContext.Set<Supplier>().AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
    }
}
