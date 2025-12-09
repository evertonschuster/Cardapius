using Administration.Domain.Ncms;
using Administration.Domain.Ncms.Entities;
using Administration.Domain.Ncms.ValueObjects;
using FluentAssertions;

namespace Administration.Domain.UnitTest.Ncms;

public class NcmTests
{
    [Fact]
    public void Create_ShouldBuildAggregate()
    {
        var dto = new NcmDto(
            "Pizza congelada",
            new NcmCode("19059090"),
            new TaxationProfile("Alimentos", 1.25m),
            new TaxEstimation(1.5m, 2.5m, 3.5m, 4.5m),
            new TableVersion("2024.01", "SP"),
            new NcmClassification("G1", "SG1", "S1", "S1.1"),
            new ValidityPeriod(new DateOnly(2024, 1, 1), new DateOnly(2024, 12, 31)),
            new TaxableUnit("KG", "Quilograma"),
            new BenefitCode("123"));

        var model = Ncm.Create(dto);

        model.Id.Should().NotBeEmpty();
        model.Description.Should().Be(dto.Description);
        model.Code.Code.Should().Be(dto.Code.Code);
        model.TaxationDescription.Should().Be(dto.TaxationProfile.TaxationDescription);
        model.FixedRate.Should().Be(dto.TaxationProfile.FixedRate);
        model.TaxEstimation.Should().Be(dto.TaxEstimation);
        model.TableVersion.Should().Be(dto.TableVersion);
        model.Group.Should().Be(dto.Classification.Group);
        model.Subgroup.Should().Be(dto.Classification.Subgroup);
        model.Section.Should().Be(dto.Classification.Section);
        model.SectionDetail.Should().Be(dto.Classification.SectionDetail);
        model.Validity.Should().Be(dto.Validity);
        model.TaxedUnit.Should().Be(dto.TaxableUnit.Unit);
        model.UnitDescription.Should().Be(dto.TaxableUnit.Description);
        model.BenefitIdentifier.Should().Be(dto.BenefitCode.Code);
    }

    [Fact]
    public void Update_ShouldReplaceState()
    {
        var model = Ncm.Create(new NcmDto(
            "Pizza congelada",
            new NcmCode("19059090"),
            new TaxationProfile("Alimentos", 1.25m),
            new TaxEstimation(1.5m, 2.5m, 3.5m, 4.5m),
            new TableVersion("2024.01", "SP"),
            new NcmClassification("G1", "SG1", "S1", "S1.1"),
            new ValidityPeriod(new DateOnly(2024, 1, 1), new DateOnly(2024, 12, 31)),
            new TaxableUnit("KG", "Quilograma"),
            new BenefitCode("123")));

        var updated = new NcmDto(
            "Molho pronto",
            new NcmCode("21039010"),
            new TaxationProfile("Condimentos", 0.75m),
            new TaxEstimation(0.5m, 1.5m, 2.5m, 3.5m),
            new TableVersion("2024.02", "RJ"),
            new NcmClassification("G2", "SG2", "S2", "S2.1"),
            new ValidityPeriod(new DateOnly(2024, 2, 1), null),
            new TaxableUnit("L", "Litro"),
            new BenefitCode("456"));

        model.Update(updated);

        model.Description.Should().Be(updated.Description);
        model.Code.Should().Be(updated.Code);
        model.TaxEstimation.Should().Be(updated.TaxEstimation);
        model.TableVersion.Should().Be(updated.TableVersion);
        model.FixedRate.Should().Be(updated.TaxationProfile.FixedRate);
        model.Section.Should().Be(updated.Classification.Section);
        model.Validity.Should().Be(updated.Validity);
        model.BenefitIdentifier.Should().Be(updated.BenefitCode.Code);
    }
}
