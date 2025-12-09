namespace Administration.Application.Ncms.Commands.UpdateNcm;

public class UpdateNcmValidator : AbstractValidator<UpdateNcmCommand>
{
    public UpdateNcmValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Identificador é obrigatório.");
        RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Descrição é obrigatória.")
                .MaximumLength(512).WithMessage("Descrição deve ter no máximo 512 caracteres.");
    }
}