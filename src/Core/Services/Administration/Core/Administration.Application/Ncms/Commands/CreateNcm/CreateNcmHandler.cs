using Administration.Domain.Ncms.Repositories;
using BuildingBlock.Application;

namespace Administration.Application.Ncms.Commands.CreateNcm;

public class CreateNcmHandler(INcmRepository repository, IUnitOfWork unitOfWork) : ICommandHandler<CreateNcmCommand, CreateNcmResult>
{
    private readonly INcmRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<CreateNcmResult>> Handle(CreateNcmCommand request, CancellationToken cancellationToken)
    {
        var model = request.ToModel();
        await _repository.SaveAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<CreateNcmResult>.Success(new CreateNcmResult(model.Id));
    }
}
