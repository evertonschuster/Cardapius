using BuildingBlock.Domain.ValueObjects;
using MediatR;

namespace BuildingBlock.Application.Commands
{
    public interface IQueryRequest<TResponse> : IRequest<Result<TResponse>>
    {

    }
}
