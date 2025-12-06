using Administration.Domain.Common.Pagination;
using Administration.Domain.Ncms.Entities;
using Administration.Domain.Ncms.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Administration.Infra.DataBase.EntityFramework.Ncms.Repositories;

public class NcmRepository(IDbContext context) : Repository<Ncm>(context), INcmRepository
{
    public Task<Ncm?> GetByIdAsync(Guid id)
    {
        return _IDbContext.Set<Ncm>()
            .AsNoTracking()
            .FirstOrDefaultAsync(ncm => ncm.Id == id);
    }

    public Task<PaginatedResult<Ncm>> ListAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        pageNumber = Math.Max(1, pageNumber);
        pageSize = Math.Max(1, pageSize);

        var query = _IDbContext.Set<Ncm>().AsNoTracking().OrderBy(n => n.Code.Code);
        return PaginateAsync(query, pageNumber, pageSize, cancellationToken);
    }

    private static async Task<PaginatedResult<Ncm>> PaginateAsync(IQueryable<Ncm> query, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<Ncm>(items, pageNumber, pageSize, totalCount);
    }
}
