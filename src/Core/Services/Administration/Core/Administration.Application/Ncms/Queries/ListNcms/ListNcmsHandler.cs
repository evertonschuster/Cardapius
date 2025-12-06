using Administration.Domain.Common.Pagination;
using Administration.Domain.Ncms.Repositories;
using System.Linq;

namespace Administration.Application.Ncms.Queries.ListNcms;

internal class ListNcmsHandler(INcmRepository repository) : IQueryHandler<ListNcmsQuery, PaginatedResult<ListNcmsResult>>
{
    private readonly INcmRepository _repository = repository;

    public async Task<Result<PaginatedResult<ListNcmsResult>>> Handle(ListNcmsQuery request, CancellationToken cancellationToken)
    {
        var pagedItems = await _repository.ListAsync(request.PageNumber, request.PageSize, cancellationToken);
        var mappedItems = pagedItems.Items.Select(ListNcmsResult.FromModel).ToList();

        var result = new PaginatedResult<ListNcmsResult>(mappedItems, pagedItems.PageNumber, pagedItems.PageSize, pagedItems.TotalCount);

        return Result<PaginatedResult<ListNcmsResult>>.Success(result);
    }
}
