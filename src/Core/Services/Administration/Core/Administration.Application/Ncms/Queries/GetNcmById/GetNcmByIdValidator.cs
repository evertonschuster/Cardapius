namespace Administration.Application.Ncms.Queries.GetNcmById;

public class GetNcmByIdValidator : AbstractValidator<GetNcmByIdQuery>
{
    public GetNcmByIdValidator()
    {
        RuleFor(query => query.Id)
            .NotEmpty()
            .WithMessage("Identificador do NCM é obrigatório.");
    }
}
