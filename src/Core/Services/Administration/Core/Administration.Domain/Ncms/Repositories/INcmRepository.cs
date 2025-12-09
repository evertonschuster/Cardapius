using Administration.Domain.Common.Pagination;
using Administration.Domain.Ncms.Entities;

namespace Administration.Domain.Ncms.Repositories;

public interface INcmRepository
{
    Task SaveAsync(Ncm model);

    Task<Ncm?> GetByIdAsync(Guid id);

    Task<PaginatedResult<Ncm>> ListAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
}
