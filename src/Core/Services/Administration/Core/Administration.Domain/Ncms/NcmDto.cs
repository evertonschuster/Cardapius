using Administration.Domain.Ncms.ValueObjects;

namespace Administration.Domain.Ncms;

public record NcmDto(
    string Description,
    NcmCode Code,
    TaxationProfile TaxationProfile,
    TaxEstimation TaxEstimation,
    TableVersion TableVersion,
    NcmClassification Classification,
    ValidityPeriod Validity,
    TaxableUnit? TaxableUnit,
    BenefitCode? BenefitCode);
