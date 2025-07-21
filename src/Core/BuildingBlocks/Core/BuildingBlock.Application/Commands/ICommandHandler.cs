using BuildingBlock.Domain.ValueObjects;
using MediatR;

namespace BuildingBlock.Application.Commands
{
    public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse>>
        where TRequest : ICommandRequest<TResponse>
    {
    }
}
