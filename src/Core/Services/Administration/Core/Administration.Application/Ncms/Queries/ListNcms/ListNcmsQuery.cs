using Administration.Domain.Common.Pagination;

namespace Administration.Application.Ncms.Queries.ListNcms;

public class ListNcmsQuery : IQueryRequest<PaginatedResult<ListNcmsResult>>
{
    public int PageNumber { get; init; } = 1;

    public int PageSize { get; init; } = 20;
}
