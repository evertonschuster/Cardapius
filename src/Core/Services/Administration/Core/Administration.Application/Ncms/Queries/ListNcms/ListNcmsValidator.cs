namespace Administration.Application.Ncms.Queries.ListNcms;

public class ListNcmsValidator : AbstractValidator<ListNcmsQuery>
{
    public ListNcmsValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .WithMessage("Número da página deve ser maior que zero.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 200)
            .WithMessage("Tamanho da página deve estar entre 1 e 200.");
    }
}
