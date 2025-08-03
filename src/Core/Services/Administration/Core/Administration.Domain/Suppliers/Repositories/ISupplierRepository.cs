using Administration.Domain.Suppliers.Entities;
using System;
using System.Collections.Generic;

namespace Administration.Domain.Suppliers.Repositories;

public interface ISupplierRepository
{
    Task SaveAsync(Supplier model);

    Task<Supplier?> GetByIdAsync(Guid id);

    Task<List<Supplier>> ListAsync();
}
