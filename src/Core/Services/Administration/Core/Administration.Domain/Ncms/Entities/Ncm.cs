using Administration.Domain.Ncms.DomainEvents;
using Administration.Domain.Ncms.ValueObjects;

namespace Administration.Domain.Ncms.Entities;

public class Ncm : Entity
{
    protected Ncm()
    {
    }

    private Ncm(
        Guid id,
        string description,
        NcmCode code,
        TaxationProfile taxationProfile,
        TaxEstimation taxEstimation,
        TableVersion tableVersion,
        NcmClassification classification,
        ValidityPeriod validity,
        TaxableUnit? taxableUnit,
        BenefitCode? benefitCode) : base(id)
    {
        Description = description;
        Code = code;
        TaxationProfile = taxationProfile;
        TaxEstimation = taxEstimation;
        TableVersion = tableVersion;
        Classification = classification;
        Validity = validity;
        TaxableUnit = taxableUnit;
        BenefitCode = benefitCode;
    }

    public string Description { get; private set; }
    public NcmCode Code { get; private set; }
    public TaxationProfile TaxationProfile { get; private set; }
    public TaxEstimation TaxEstimation { get; private set; }
    public TableVersion TableVersion { get; private set; }
    public NcmClassification Classification { get; private set; }
    public ValidityPeriod Validity { get; private set; }
    public TaxableUnit? TaxableUnit { get; private set; }
    public BenefitCode? BenefitCode { get; private set; }

    public string TaxationDescription => TaxationProfile.TaxationDescription;
    public decimal? FixedRate => TaxationProfile.FixedRate;
    public string? Group => Classification.Group;
    public string? Subgroup => Classification.Subgroup;
    public string? Section => Classification.Section;
    public string? SectionDetail => Classification.SectionDetail;
    public string? TaxedUnit => TaxableUnit?.Unit;
    public string? UnitDescription => TaxableUnit?.Description;
    public string? BenefitIdentifier => BenefitCode?.Code;

    public static Ncm Create(NcmDto dto)
    {
        var model = new Ncm(
            Guid.CreateVersion7(),
            dto.Description,
            dto.Code,
            dto.TaxationProfile,
            dto.TaxEstimation,
            dto.TableVersion,
            dto.Classification,
            dto.Validity,
            dto.TaxableUnit,
            dto.BenefitCode);

        model.AddDomainEvent(new NcmCreatedEvent<Ncm>(model.Id, null, model));
        return model;
    }

    public void Update(NcmDto dto)
    {
        var before = (Ncm)MemberwiseClone();

        Description = dto.Description;
        Code = dto.Code;
        TaxationProfile = dto.TaxationProfile;
        TaxEstimation = dto.TaxEstimation;
        TableVersion = dto.TableVersion;
        Classification = dto.Classification;
        Validity = dto.Validity;
        TaxableUnit = dto.TaxableUnit;
        BenefitCode = dto.BenefitCode;

        AddDomainEvent(new NcmUpdatedEvent<Ncm>(Id, before, this));
    }
}
