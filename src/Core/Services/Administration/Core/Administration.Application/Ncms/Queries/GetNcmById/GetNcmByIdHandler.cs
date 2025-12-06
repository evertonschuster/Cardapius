using Administration.Domain.Ncms.Repositories;

namespace Administration.Application.Ncms.Queries.GetNcmById;

internal class GetNcmByIdHandler(INcmRepository repository) : IQueryHandler<GetNcmByIdQuery, GetNcmByIdResult>
{
    private readonly INcmRepository _repository = repository;

    public async Task<Result<GetNcmByIdResult>> Handle(GetNcmByIdQuery request, CancellationToken cancellationToken)
    {
        var model = await _repository.GetByIdAsync(request.Id);
        if (model is null)
        {
            return Result<GetNcmByIdResult>.Fail(nameof(request.Id), $"NCM com ID {request.Id} não encontrada.");
        }

        var result = GetNcmByIdResult.FromModel(model);
        return Result<GetNcmByIdResult>.Success(result);
    }
}
