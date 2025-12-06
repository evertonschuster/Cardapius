using Administration.Domain.Ncms.ValueObjects;
using FluentValidation;

namespace Administration.Application.Ncms.Commands;

public interface INcmCommandBase
{
    string Description { get; set; }
    NcmCode Code { get; set; }
    TaxationProfile TaxationProfile { get; set; }
    TaxEstimation TaxEstimation { get; set; }
    TableVersion TableVersion { get; set; }
    NcmClassification Classification { get; set; }
    ValidityPeriod Validity { get; set; }
    TaxableUnit? TaxableUnit { get; set; }
    BenefitCode? BenefitCode { get; set; }
}

public abstract class NcmCommandValidator<TCommand> : AbstractValidator<TCommand>
    where TCommand : INcmCommandBase
{
    protected NcmCommandValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Descrição é obrigatória.")
            .MaximumLength(512).WithMessage("Descrição deve ter no máximo 512 caracteres.");

        RuleFor(x => x.Code)
            .NotNull().WithMessage("Código NCM é obrigatório.")
            .DependentRules(() =>
            {
                RuleFor(x => x.Code.Code)
                    .NotEmpty().WithMessage("Código NCM é obrigatório.")
                    .MaximumLength(20).WithMessage("Código NCM deve ter no máximo 20 caracteres.");
            });

        RuleFor(x => x.TaxationProfile)
            .NotNull().WithMessage("Tributação é obrigatória.")
            .DependentRules(() =>
            {
                RuleFor(x => x.TaxationProfile.TaxationDescription)
                    .NotEmpty().WithMessage("Descrição da tributação é obrigatória.")
                    .MaximumLength(128).WithMessage("Descrição da tributação deve ter no máximo 128 caracteres.");

                RuleFor(x => x.TaxationProfile.FixedRate)
                    .GreaterThanOrEqualTo(0)
                    .When(x => x.TaxationProfile.FixedRate.HasValue)
                    .WithMessage("Alíquota fixa deve ser maior ou igual a zero.");
            });

        RuleFor(x => x.TaxEstimation)
            .NotNull().WithMessage("Estimativas de tributo são obrigatórias.")
            .DependentRules(() =>
            {
                RuleFor(x => x.TaxEstimation.National).GreaterThanOrEqualTo(0).WithMessage("Tributo nacional deve ser maior ou igual a zero.");
                RuleFor(x => x.TaxEstimation.Imported).GreaterThanOrEqualTo(0).WithMessage("Tributo importado deve ser maior ou igual a zero.");
                RuleFor(x => x.TaxEstimation.State).GreaterThanOrEqualTo(0).WithMessage("Tributo estadual deve ser maior ou igual a zero.");
                RuleFor(x => x.TaxEstimation.Municipal).GreaterThanOrEqualTo(0).WithMessage("Tributo municipal deve ser maior ou igual a zero.");
            });

        RuleFor(x => x.TableVersion)
            .NotNull().WithMessage("Versão da tabela é obrigatória.")
            .DependentRules(() =>
            {
                RuleFor(x => x.TableVersion.Version)
                    .NotEmpty().WithMessage("Versão da tabela é obrigatória.")
                    .MaximumLength(32).WithMessage("Versão da tabela deve ter no máximo 32 caracteres.");

                RuleFor(x => x.TableVersion.FederativeUnit)
                    .MaximumLength(2)
                    .WithMessage("Unidade federativa deve ter no máximo 2 caracteres.")
                    .When(x => x.TableVersion.FederativeUnit is not null);
            });

        RuleFor(x => x.Classification)
            .NotNull().WithMessage("Classificação é obrigatória.")
            .DependentRules(() =>
            {
                RuleFor(x => x.Classification.Group).MaximumLength(64).WithMessage("Grupo deve ter no máximo 64 caracteres.");
                RuleFor(x => x.Classification.Subgroup).MaximumLength(64).WithMessage("Subgrupo deve ter no máximo 64 caracteres.");
                RuleFor(x => x.Classification.Section).MaximumLength(64).WithMessage("Seção deve ter no máximo 64 caracteres.");
                RuleFor(x => x.Classification.SectionDetail).MaximumLength(64).WithMessage("Detalhe da seção deve ter no máximo 64 caracteres.");
            });

        RuleFor(x => x.Validity)
            .NotNull().WithMessage("Vigência é obrigatória.")
            .DependentRules(() =>
            {
                RuleFor(x => x.Validity.End)
                    .GreaterThanOrEqualTo(x => x.Validity.Start)
                    .When(x => x.Validity.End.HasValue)
                    .WithMessage("Data final deve ser maior ou igual à data inicial.");
            });

        When(x => x.TaxableUnit is not null, () =>
        {
            RuleFor(x => x.TaxableUnit!.Unit)
                .NotEmpty().WithMessage("Unidade tributada é obrigatória quando informada.")
                .MaximumLength(64).WithMessage("Unidade tributada deve ter no máximo 64 caracteres.");

            RuleFor(x => x.TaxableUnit!.Description)
                .NotEmpty().WithMessage("Descrição da unidade tributada é obrigatória quando informada.")
                .MaximumLength(256).WithMessage("Descrição da unidade tributada deve ter no máximo 256 caracteres.");
        });

        When(x => x.BenefitCode is not null, () =>
        {
            RuleFor(x => x.BenefitCode!.Code)
                .NotEmpty().WithMessage("Código de benefício é obrigatório quando informado.")
                .MaximumLength(64).WithMessage("Código de benefício deve ter no máximo 64 caracteres.");
        });
    }
}
