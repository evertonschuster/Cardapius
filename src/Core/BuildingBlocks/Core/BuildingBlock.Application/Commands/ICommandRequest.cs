using BuildingBlock.Domain.ValueObjects;
using MediatR;

namespace BuildingBlock.Application.Commands
{
    public interface ICommandRequest<TResponse> : IRequest<Result<TResponse>>
    {

    }
}
