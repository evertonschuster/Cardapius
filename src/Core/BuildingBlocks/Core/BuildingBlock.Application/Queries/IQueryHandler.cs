using BuildingBlock.Application.Queries;
using BuildingBlock.Domain.ValueObjects;
using MediatR;

namespace BuildingBlock.Application.Commands
{
    public interface IQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse>>
        where TRequest : IQueryRequest<TResponse>
    {
    }
}
