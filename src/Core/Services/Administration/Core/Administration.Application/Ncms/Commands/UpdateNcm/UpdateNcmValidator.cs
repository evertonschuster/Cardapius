using Administration.Application.Ncms.Commands;

namespace Administration.Application.Ncms.Commands.UpdateNcm;

public class UpdateNcmValidator : NcmCommandValidator<UpdateNcmCommand>
{
    public UpdateNcmValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Identificador é obrigatório.");
    }
}
