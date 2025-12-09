using Administration.Domain.Ncms.Entities;

namespace Administration.Application.Ncms.Queries.ListNcms;

public record ListNcmsResult(
    Guid Id,
    string Description,
    string Code,
    string TaxationDescription,
    decimal? FixedRate,
    decimal National,
    decimal Imported,
    decimal State,
    decimal Municipal,
    string TableVersion,
    string? TableFederativeUnit,
    string? Group,
    string? Subgroup,
    string? Section,
    string? SectionDetail,
    DateOnly ValidityStart,
    DateOnly? ValidityEnd,
    string? TaxedUnit,
    string? UnitDescription,
    string? BenefitCode)
{
    public static ListNcmsResult FromModel(Ncm model) => new(
        model.Id,
        model.Description,
        model.Code.Code,
        model.TaxationDescription,
        model.FixedRate,
        model.TaxEstimation.National,
        model.TaxEstimation.Imported,
        model.TaxEstimation.State,
        model.TaxEstimation.Municipal,
        model.TableVersion.Version,
        model.TableVersion.FederativeUnit,
        model.Group,
        model.Subgroup,
        model.Section,
        model.SectionDetail,
        model.Validity.Start,
        model.Validity.End,
        model.TaxedUnit,
        model.UnitDescription,
        model.BenefitIdentifier);
}
