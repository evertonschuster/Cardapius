using BuildingBlock.Domain.ValueObjects;
using MediatR;

namespace BuildingBlock.Application.Queries
{
    public interface IQueryRequest<TResponse> : IRequest<Result<TResponse>>
    {

    }
}
