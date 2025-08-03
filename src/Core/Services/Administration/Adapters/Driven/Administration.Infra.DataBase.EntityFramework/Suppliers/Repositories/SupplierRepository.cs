using Administration.Domain.Suppliers.Entities;
using Administration.Domain.Suppliers.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Administration.Infra.DataBase.EntityFramework.Suppliers.Repositories;

public class SupplierRepository : Repository<Supplier>, ISupplierRepository
{
    public SupplierRepository(IDbContext context) : base(context)
    {
    }

    public Task<Supplier?> GetByIdAsync(Guid id)
    {
        return base.GetByIdAsync(id);
    }

    public Task<List<Supplier>> ListAsync()
    {
        return _IDbContext.Set<Supplier>().AsNoTracking().ToListAsync();
    }
}
