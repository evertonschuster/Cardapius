using Administration.Domain.Suppliers.Repositories;

namespace Administration.Application.Suppliers.Queries.ListSuppliers;

internal class ListSuppliersHandler(ISupplierRepository repository) : IQueryHandler<ListSuppliersQuery, List<ListSuppliersResult>>
{
    public async Task<Result<List<ListSuppliersResult>>> Handle(ListSuppliersQuery request, CancellationToken cancellationToken)
    {
        var suppliers = await repository.ListAsync();
        var result = suppliers.Select(ListSuppliersResult.FromModel).ToList();
        return Result<List<ListSuppliersResult>>.Success(result);
    }
}

