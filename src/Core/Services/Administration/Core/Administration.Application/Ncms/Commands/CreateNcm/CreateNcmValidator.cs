namespace Administration.Application.Ncms.Commands.CreateNcm;

public class CreateNcmValidator : AbstractValidator<CreateNcmCommand>
{
    public CreateNcmValidator()
    {
        RuleFor(x => x.Description)
            .MaximumLength(512).WithMessage("Descrição deve ter no máximo 512 caracteres.");

    }
}
