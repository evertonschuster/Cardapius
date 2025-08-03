using Administration.Domain.Suppliers;
using Administration.Domain.Suppliers.Entities;
using BuildingBlock.Domain.ValueObjects.Business;

namespace Administration.Application.Suppliers.Queries.ListSuppliers;

public record ListSuppliersResult(Guid Id, LegalName LegalName, TradeName TradeName, SupplierStatus Status)
{
    public static ListSuppliersResult FromModel(Supplier supplier)
    {
        return new ListSuppliersResult(supplier.Id, supplier.LegalName, supplier.TradeName, supplier.Status);
    }
}

