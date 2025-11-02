using Administration.Domain.Suppliers.Entities;

namespace Administration.Domain.Suppliers.Repositories;

public interface ISupplierRepository
{
    Task SaveAsync(Supplier model);

    Task<Supplier?> GetByIdAsync(Guid id);

    Task<List<Supplier>> ListAsync(CancellationToken cancellationToken);
}
