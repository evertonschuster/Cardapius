namespace Administration.Application.Ncms.Queries.GetNcmById;

public record GetNcmByIdQuery(Guid Id) : IQueryRequest<GetNcmByIdResult>;
