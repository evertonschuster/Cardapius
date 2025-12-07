using Administration.Domain.Ncms;
using Administration.Domain.Ncms.Entities;
using Administration.Domain.Ncms.ValueObjects;

namespace Administration.Application.Ncms.Commands.CreateNcm;

public class CreateNcmCommand : ICommandRequest<CreateNcmResult>, INcmCommandBase
{
    public string Description { get; set; } = string.Empty;
    public NcmCode Code { get; set; }
    public TaxationProfile TaxationProfile { get; set; }
    public TaxEstimation TaxEstimation { get; set; }
    public TableVersion TableVersion { get; set; }
    public NcmClassification Classification { get; set; }
    public ValidityPeriod Validity { get; set; }
    public TaxableUnit? TaxableUnit { get; set; }
    public BenefitCode? BenefitCode { get; set; }

    internal NcmDto ToDto() => new(
        Description,
        Code,
        TaxationProfile,
        TaxEstimation,
        TableVersion,
        Classification,
        Validity,
        TaxableUnit,
        BenefitCode);

    internal Ncm ToModel() => Ncm.Create(ToDto());
}
