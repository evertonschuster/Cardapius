using Administration.Domain.Ncms.Repositories;
using BuildingBlock.Application;

namespace Administration.Application.Ncms.Commands.UpdateNcm;

public class UpdateNcmHandler(INcmRepository repository, IUnitOfWork unitOfWork) : ICommandHandler<UpdateNcmCommand, UpdateNcmResult>
{
    private readonly INcmRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<UpdateNcmResult>> Handle(UpdateNcmCommand request, CancellationToken cancellationToken)
    {
        var model = await _repository.GetByIdAsync(request.Id);
        if (model is null)
        {
            return Result<UpdateNcmResult>.Fail(nameof(request.Id), $"NCM com ID {request.Id} não encontrada.");
        }

        model.Update(request.ToDto());
        await _repository.SaveAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<UpdateNcmResult>.Success(new UpdateNcmResult());
    }
}
