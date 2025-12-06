using Administration.Application.Ncms.Commands.CreateNcm;

namespace Administration.Application.Ncms.Commands.UpdateNcm;

public class UpdateNcmValidator : AbstractValidator<UpdateNcmCommand>
{
    public UpdateNcmValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Identificador é obrigatório.");
        Include(new CreateNcmValidator());
    }
}
